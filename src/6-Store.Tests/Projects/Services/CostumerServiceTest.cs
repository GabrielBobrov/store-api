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
using EscNet.Mails.Interfaces;

namespace Store.Tests.Projects.Services
{
    public class CostumerServiceTests
    {
        // Subject Under Test (Quem será testado!)
        private readonly ICostumerServices _sut;

        //Mocks
        private readonly IMapper _mapper;
        private readonly Mock<ICostumerRepository> _costumerRepositoryMock;
        private readonly Mock<IOrderRepository> _orderRepositoryMock;

        private readonly Mock<IMediatorHandler> _mediatorHandler;

        private readonly Mock<IEmailSender> _emailSender;

        public CostumerServiceTests()
        {
            _mapper = AutoMapperConfiguration.GetConfiguration();
            _costumerRepositoryMock = new Mock<ICostumerRepository>();
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _mediatorHandler = new Mock<IMediatorHandler>();
            _emailSender = new Mock<IEmailSender>();

            _sut = new CostumerServices(
                mapper: _mapper,
                costumerRepository: _costumerRepositoryMock.Object,
                orderRepository: _orderRepositoryMock.Object,
                mediator: _mediatorHandler.Object,
                emailSender: _emailSender.Object);
        }

        #region Create

        [Fact(DisplayName = "Create Valid Costumer")]
        [Trait("Category", "Services")]
        public async Task Create_WhenCostumerIsValid_ReturnsCostumerDto()
        {
            // Arrange
            var costumerToCreate = CostumerFixture.CreateValidCostumerDto();

            var costumerCreated = _mapper.Map<Costumer>(costumerToCreate);

            _costumerRepositoryMock.Setup(x => x.GetAsync(
                It.IsAny<Expression<Func<Costumer, bool>>>(),
                It.IsAny<bool>()))
            .ReturnsAsync(() => null);

            _costumerRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<Costumer>()))
                .ReturnsAsync(() => costumerCreated);

            // Act
            var result = await _sut.CreateAsync(costumerToCreate);

            // Assert
            result.Value.Should()
                .BeEquivalentTo(_mapper.Map<CostumerDto>(costumerCreated));
        }

        [Fact(DisplayName = "Create When Costumer Exists")]
        [Trait("Category", "Services")]
        public async Task Create_WhenCostumerExists_ReturnsEmptyOptional()
        {
            // Arrange
            var costumerToCreate = CostumerFixture.CreateValidCostumerDto();
            var costumerExists = CostumerFixture.CreateValidCostumer();

            _costumerRepositoryMock.Setup(x => x.GetAsync(
                It.IsAny<Expression<Func<Costumer, bool>>>(),
                It.IsAny<bool>()))
            .ReturnsAsync(() => costumerExists);

            // Act
            var result = await _sut.CreateAsync(costumerToCreate);


            // Act
            result.HasValue.Should()
                .BeFalse();
        }

        [Fact(DisplayName = "Create When Costumer is Invalid")]
        [Trait("Category", "Services")]
        public async Task Create_WhenCostumerIsInvalid_ReturnsEmptyOptional()
        {
            // Arrange
            var costumerToCreate = CostumerFixture.CreateInvalidCostumerDto();

            _costumerRepositoryMock.Setup(x => x.GetAsync(
                It.IsAny<Expression<Func<Costumer, bool>>>(),
                It.IsAny<bool>()))
            .ReturnsAsync(() => null);

            // Act
            var result = await _sut.CreateAsync(costumerToCreate);


            // Act
            result.HasValue.Should()
                .BeFalse();
        }

        #endregion

        #region Get

        [Fact(DisplayName = "Get By Id")]
        [Trait("Category", "Services")]
        public async Task GetById_WhenCostumerExists_ReturnsCostumerDto()
        {
            // Arrange
            var costumerId = new Randomizer().Int(0, 1000);
            var costumerFound = CostumerFixture.CreateValidCostumer();

            _costumerRepositoryMock.Setup(x => x.GetAsync(costumerId))
            .ReturnsAsync(() => costumerFound);

            _orderRepositoryMock.Setup(x => x.SearchAsync(
                It.IsAny<Expression<Func<Order, bool>>>(),
                It.IsAny<bool>()))
            .ReturnsAsync(() => new List<Order>());

            // Act
            var result = await _sut.GetAsync(costumerId);

            // Assert
            result.Value.Should()
                .BeEquivalentTo(_mapper.Map<CostumerDto>(costumerFound));
        }

        [Fact(DisplayName = "Get All Costumers")]
        [Trait("Category", "Services")]
        public async Task GetAllCostumers_WhenCostumersExists_ReturnsAListOfCostumerDto()
        {
            // Arrange
            var costumersFound = CostumerFixture.CreateListValidCostumers();

            _costumerRepositoryMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(() => costumersFound);

            // Act
            var result = await _sut.GetAllAsync();

            // Assert
            result.Value.Should()
                .BeEquivalentTo(_mapper.Map<List<CostumerDto>>(costumersFound));
        }

        [Fact(DisplayName = "Get All Costumers When None Costumer Found")]
        [Trait("Category", "Services")]
        public async Task GetAllCostumers_WhenNoneCostumerFound_ReturnsEmptyList()
        {
            // Arrange

            _costumerRepositoryMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(() => null);

            // Act
            var result = await _sut.GetAllAsync();

            // Assert
            result.Value.Should()
                .BeEmpty();
        }

        #endregion

    }
}