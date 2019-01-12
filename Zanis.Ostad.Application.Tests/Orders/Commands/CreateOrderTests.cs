using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MockQueryable.Moq;
using Moq;
using Xunit;
using Xunit.Abstractions;
using Zains.Ostad.Application.Orders.Commands;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Dtos;
using Zanis.Ostad.Core.Dtos.Orders;
using Zanis.Ostad.Core.Entities;
using Zanis.Ostad.Core.Entities.Cart;
using Zanis.Ostad.Core.Entities.Orders;
using Zanis.Ostad.Payment.Refah;

namespace Zanis.Ostad.Application.Tests.Orders.Commands
{
    public class CreateOrderTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly Mock<IRepository<Order, long>> _orderRepository;
        private readonly Mock<IRepository<OrderItem, long>> _orderItemRepository;
        private readonly Mock<IRepository<CartItem, Guid>> _cartItemRepository;
        private readonly Mock<IOrderPaymentProviderFactory> _paymentProviderFactory;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IPaymentProvider> _paymentProvider;
        private readonly CreateOrderCommandHandler _handler;

        public CreateOrderTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _paymentProviderFactory = new Mock<IOrderPaymentProviderFactory>();
            _paymentProvider = new Mock<IPaymentProvider>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _cartItemRepository = new Mock<IRepository<CartItem, Guid>>();
            _orderRepository = new Mock<IRepository<Order, long>>();
            _orderItemRepository = new Mock<IRepository<OrderItem, long>>();
            _handler = new CreateOrderCommandHandler(_cartItemRepository.Object, _orderRepository.Object,
            _paymentProviderFactory.Object,_unitOfWork.Object);
        }

        [Fact]
        public async Task ShouldCreateSayFailIfCartIsEmpty()
        {
            var userId = 1;

            _cartItemRepository.Setup(x => x.GetQueriable())
                .Returns(new List<CartItem>().AsQueryable().BuildMock().Object);
            var res = await _handler.Handle(new CreateOrderCommand
            {
                UserId = userId
            }, CancellationToken.None);
            Assert.Equal(ResponseStatus.UnKnown, res.Status);
        }

        [Fact]
        public async Task ShouldCreateOrderFromCartItemsAndRemoveCartItems()
        {
            var userId = 1;
            InitCart(userId);
            _paymentProvider.Setup(x => x.CreatePayment(It.IsAny<Order>(),It.IsAny<string>()))
                .Returns(new OrderPayRequestResponse()
                {
                    Status = ResponseStatus.Success,
                });
            _paymentProviderFactory
                .Setup(x => x.GetProvider(It.IsAny<string>())).Returns(_paymentProvider.Object);
            var res = await _handler.Handle(new CreateOrderCommand
            {
                UserId = userId,
            }, CancellationToken.None);
            Assert.Equal(ResponseStatus.Success, res.Status);
            _orderRepository.Verify(x => x.AddAsync(It.IsAny<Order>()));
            _cartItemRepository.Verify(x => x.Delete(It.IsAny<Guid>()));
        }

        [Fact]
        public async Task ShouldReturnsAResponseWithPaymentResultBaseOnRequestPaymentStrategy()
        {
            var userId = 1;
            InitCart(userId);
            _paymentProvider.Setup(x => x.CreatePayment(It.IsAny<Order>(),It.IsAny<string>()))
                .Returns(new RefahPaymentRequestResult()
                {
                    Status = ResponseStatus.Success,
                });
            _paymentProviderFactory
                .Setup(x => x.GetProvider(It.IsAny<string>())).Returns(_paymentProvider.Object);
            var res = await _handler.Handle(new CreateOrderCommand
            {
                PaymentStrategy = "SomeDumbPaymentStrategy",
                UserId = userId,
            }, CancellationToken.None);
            Assert.Equal(ResponseStatus.Success, res.Status);
            Assert.IsType<RefahPaymentRequestResult>(res);
        }

        private void InitCart(int userId)
        {
            _cartItemRepository.Setup(x => x.GetQueriable())
                .Returns(new List<CartItem>
                {
                    new CartItem
                    {
                        ItemType = ProductType.LessonExam,
                        LessonExamId = 5,
                        UserId = userId,
                        LessonExam = new Lesson
                        {
                            ExamSamplesPrice = 12000
                        }
                    }
                }.AsQueryable().BuildMock().Object);
        }
    }
}