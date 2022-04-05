using AutoMapper;
using Bogus;
using Bogus.DataSets;
using EscNet.Cryptography.Interfaces;
using FluentAssertions;
using Store.Core.Communication.Mediator.Interfaces;
using Store.Domain.Entities;
using Store.Infra.Interfaces;
using Store.Services.DTO;
using Store.Services.Interfaces;
using Store.Services.Services;
using Store.Tests.Configurations.AutoMapper;
using Store.Tests.Fixtures;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using System.Linq.Expressions;

namespace Store.Tests.Projects.Services
{
    public class OrderServiceTest
    {
        // Subject Under Test (Quem será testado!)
        private readonly IOrderServices _sut;

        //Mocks
        private readonly IMapper _mapper;
        private readonly Mock<ICostumerRepository> _costumerRepositoryMock;
        private readonly Mock<IOrderRepository> _orderRepositoryMock;

        private readonly Mock<IMediatorHandler> _mediatorHandler;

        public OrderServiceTest()
        {
            _mapper = AutoMapperConfiguration.GetConfiguration();
            _costumerRepositoryMock = new Mock<ICostumerRepository>();
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _mediatorHandler = new Mock<IMediatorHandler>();

            _sut = new OrderService(
                mapper: _mapper,
                costumerRepository: _costumerRepositoryMock.Object,
                orderRepository: _orderRepositoryMock.Object,
                mediator: _mediatorHandler.Object);
        }

        #region Create

        [Fact(DisplayName = "Create Valid Order")]
        [Trait("Category", "Services")]
        public async Task Create_WhenOrderIsValid_ReturnsOrderDto()
        {
            // Arrange
            var orderToCreate = OrderFixture.CreateValidOrderDto();
            var costumer = CostumerFixture.CreateValidCostumer();

            var orderCreated = _mapper.Map<Order>(orderToCreate);

            _costumerRepositoryMock.Setup(x => x.GetAsync(
                It.IsAny<Expression<Func<Costumer, bool>>>(),
                It.IsAny<bool>()))
            .ReturnsAsync(() => costumer);

            _orderRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<Order>()))
                .ReturnsAsync(() => orderCreated);

            // Act
            var result = await _sut.CreateAsync(orderToCreate);

            // Assert
            result.Value.Should()
                .BeEquivalentTo(_mapper.Map<OrderDto>(orderCreated));
        }

        [Fact(DisplayName = "Create Invalid Order")]
        [Trait("Category", "Services")]
        public async Task Create_WhenOrderIsInValid_ReturnsOrderDto()
        {
            // Arrange
            var orderToCreate = OrderFixture.CreateInvalidOrderDto();
            var costumer = CostumerFixture.CreateValidCostumer();

            var orderCreated = _mapper.Map<Order>(orderToCreate);

            _costumerRepositoryMock.Setup(x => x.GetAsync(
                It.IsAny<Expression<Func<Costumer, bool>>>(),
                It.IsAny<bool>()))
            .ReturnsAsync(() => costumer);

            // Act
            var result = await _sut.CreateAsync(orderToCreate);

            // Assert
            result.HasValue.Should()
                .BeFalse();
        }

        #endregion

    }
}