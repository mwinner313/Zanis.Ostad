using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Dtos;
using Zanis.Ostad.Core.Entities;
using Zanis.Ostad.Core.Entities.Cart;
using Zanis.Ostad.Core.Entities.Orders;

namespace Zains.Ostad.Application.Orders.Commands
{
    public class ConfirmOrderPaymentCommandHandler : IRequestHandler<ConfirmOrderPaymentCommand, Response>
    {
        private readonly IOrderPaymentProviderFactory _paymentProviderFactory;
        private readonly IRepository<CartItem, Guid> _cartRepository;
        private readonly IRepository<Order, long> _orderRepository;
        private readonly IRepository<StudentExamSampleMapping, long> _studentExamRepo;
        private readonly IRepository<StudentCourseMapping, long> _studentCourseRepo;

        public ConfirmOrderPaymentCommandHandler(IOrderPaymentProviderFactory paymentProviderFactory,
            IRepository<Order, long> orderRepository, IRepository<StudentExamSampleMapping, long> studentExamRepo,
            IRepository<StudentCourseMapping, long> studentCourseRepo, IRepository<CartItem, Guid> cartRepository)
        {
            _paymentProviderFactory = paymentProviderFactory;
            _orderRepository = orderRepository;
            _studentExamRepo = studentExamRepo;
            _studentCourseRepo = studentCourseRepo;
            _cartRepository = cartRepository;
        }

        public async Task<Response> Handle(ConfirmOrderPaymentCommand request, CancellationToken cancellationToken)
        {
            var order = await GetOrder(request);
            var res = _paymentProviderFactory.GetProvider(request.PaymentStrategy).Confirm(request.context, order);
            if (res.Status == ResponseStatus.Fail)
            {
                await UpdateOrderStatusAborted(order);
                return Response.Failed("پرداخت ناموفق");
            }
            await ClearUserCart(order.UserId);
            await ActivateOrderItemsForUser(order);
            await UpdateOrderStatusPaid(order);
            return Response.Success();
        }
        private async Task ClearUserCart(long userId)
        {
            var cartItems = _cartRepository.GetQueryable().Where(x => x.UserId == userId).ToList();
            foreach (var cartItem in cartItems)
            {
                await _cartRepository.Delete(cartItem.Id);
            }
        }
        private async Task<Order> GetOrder(ConfirmOrderPaymentCommand request)
        {
            return await _orderRepository.GetQueryable().Include(x => x.OrderItems)
                .FirstAsync(x => x.Id == request.context.OrderId);
        }

        private async Task UpdateOrderStatusAborted(Order order)
        {
            order.PaymentStatus = PaymentStatus.Aborted;
            await _orderRepository.EditAsync(order);
        }

        private async Task UpdateOrderStatusPaid(Order order)
        {
            order.PaymentStatus = PaymentStatus.Paid;
            await _orderRepository.EditAsync(order);
        }

        private async Task ActivateOrderItemsForUser(Order order)
        {
            foreach (var orderItem in order.OrderItems)
            {
                switch (orderItem.ProductType)
                {
                    case ProductType.LessonExam:
                        await _studentExamRepo.AddAsync(new StudentExamSampleMapping()
                            {StudentId = order.UserId, LessonId = orderItem.ProductId});
                        break;
                    case ProductType.TeacherCourse:
                        await _studentCourseRepo.AddAsync(new StudentCourseMapping()
                            {StudentId = order.UserId, CourseId = orderItem.ProductId});
                        break;
                }
            }
        }
    }
}