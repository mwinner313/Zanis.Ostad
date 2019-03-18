using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Dtos;
using Zanis.Ostad.Core.Dtos.Orders;
using Zanis.Ostad.Core.Entities.Cart;
using Zanis.Ostad.Core.Entities.Orders;

namespace Zains.Ostad.Application.Orders.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderPayRequestResponse>
    {
        private readonly IRepository<CartItem, Guid> _cartRepository;
        private readonly IRepository<Order, long> _orderRepository;
        private readonly IOrderPaymentProviderFactory _paymentProviderFactory;
        private readonly IUnitOfWork _unitOfWork;
        public CreateOrderCommandHandler(IRepository<CartItem, Guid> cartRepository,
            IRepository<Order, long> orderRepository, IOrderPaymentProviderFactory paymentProviderFactory, IUnitOfWork unitOfWork)
        {
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _paymentProviderFactory = paymentProviderFactory;
            _unitOfWork = unitOfWork;
        }

        public async Task<OrderPayRequestResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var cartItems = GetCartItems(request.UserId);
            if (cartItems is null || !cartItems.Any())
                return UnKnownBecauseNoCartItemAvailableForThisUser();
            var orderItems = CreateOrderItems(cartItems);
            try
            {
                _unitOfWork.Begin();
           
                var order = CreateOrder(request.UserId, orderItems);
                await _orderRepository.AddAsync(order);
                _unitOfWork.Commit();
                return _paymentProviderFactory.GetProvider(request.PaymentStrategy).CreatePayment(order,request.ReturnUrl);
            }
            catch (Exception e)
            {
                _unitOfWork.RollBack();
                return new OrderPayRequestResponse
                {
                    Status = ResponseStatus.Fail
                };
            }
        }

        private OrderPayRequestResponse UnKnownBecauseNoCartItemAvailableForThisUser()
        {
            return new OrderPayRequestResponse
            {
                Status = ResponseStatus.UnKnown,
                Message = "سبد خرید شما خالیست"
            };
        }

        private Order CreateOrder(long userId, List<OrderItem> orderItems)
        {
            return new Order()
            {
                UserId = userId,
                OrderItems = orderItems
            };
        }

        private List<OrderItem> CreateOrderItems(List<CartItem> cartItems)
        {
            return cartItems.Select(x =>
            {
                var orderItem = new OrderItem
                {
                    ProductType = x.ItemType
                };
                switch (x.ItemType)
                {
                    case ProductType.LessonExam:
                        orderItem.ProductId = x.LessonExamId.Value;
                        orderItem.Price = x.LessonExam.ExamSamplesPrice;
                        break;
                    case ProductType.TeacherCourse:
                        orderItem.ProductId = x.CourseId.Value;
                        orderItem.Price = x.Course.Price;
                        break;
                }

                return orderItem;
            }).ToList();
        }

     

        private List<CartItem> GetCartItems(long userId)
        {
            return _cartRepository
                .GetQueryable()
                .Where(x => x.UserId == userId)
                .Include(x => x.Course)
                .Include(x => x.LessonExam)
                .ToList();
        }
    }
}