using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using EscNet.Cryptography.Interfaces;
using Store.Core.Communication.Mediator.Interfaces;
using Store.Core.Communication.Messages.Notifications;
using Store.Core.Enums;
using Store.Core.Structs;
using Store.Core.Validations.Message;
using Store.Domain.Entities;
using Store.Infra.Interfaces;
using Store.Services.DTO;
using Store.Services.Interfaces;

namespace Store.Services.Services
{
    public class CostumerServices : ICostumerServices
    {
        private readonly IMapper _mapper;
        private readonly ICostumerRepository _costumerRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMediatorHandler _mediator;

        public CostumerServices(
            IMapper mapper,
            ICostumerRepository costumerRepository,
            IOrderRepository orderRepository,
            IMediatorHandler mediator)
        {
            _mapper = mapper;
            _costumerRepository = costumerRepository;
            _orderRepository = orderRepository;
            _mediator = mediator;
        }

        public async Task<Optional<CostumerDto>> CreateAsync(CostumerDto costumerDto)
        {
            Expression<Func<Costumer, bool>> filter = op
                => op.Name.ToLower() == costumerDto.Name.ToLower();

            var costumerExists = await _costumerRepository.GetAsync(filter);

            if (costumerExists != null)
            {
                await _mediator.PublishDomainNotificationAsync(new DomainNotification(
                    ErrorMessages.CostumerAlreadyExists,
                    DomainNotificationType.CostumerAlreadyExists));

                return new Optional<CostumerDto>();
            }

            var costumer = _mapper.Map<Costumer>(costumerDto);
            costumer.Validate();

            if (!costumer.IsValid)
            {
                await _mediator.PublishDomainNotificationAsync(new DomainNotification(
                   ErrorMessages.CostumerInvalid(costumer.ErrorsToString()),
                   DomainNotificationType.CostumerInvalid));

                return new Optional<CostumerDto>();
            }

            var costumerCreated = await _costumerRepository.CreateAsync(costumer);

            return _mapper.Map<CostumerDto>(costumerCreated);
        }

        public async Task<Optional<IList<CostumerDto>>> GetAllAsync()
        {
            var allcostumers = await _costumerRepository.GetAllAsync();
            var allCostumersDto = _mapper.Map<IList<CostumerDto>>(allcostumers);

            return new Optional<IList<CostumerDto>>(allCostumersDto);
        }

        public async Task<Optional<CostumerDto>> GetAsync(long id)
        {
            Expression<Func<Order, bool>> filter = op
               => op.Costumer.Id == id;

            var costumer = await _costumerRepository.GetAsync(id);

            if (costumer == null)
            {
                await _mediator.PublishDomainNotificationAsync(new DomainNotification(
                    ErrorMessages.CostumerNotFound,
                    DomainNotificationType.CostumerNotFound));

                return new Optional<CostumerDto>();
            }

            var orders = await _orderRepository.SearchAsync(filter);

            var costumerDto = _mapper.Map<CostumerDto>(costumer);

            costumerDto.Orders = new List<OrderDto>();

            foreach (var o in orders)
            {
                var orderDto = _mapper.Map<OrderDto>(o);
                costumerDto.Orders.Add(orderDto);
            }

            return new Optional<CostumerDto>(costumerDto);
        }
    }
}