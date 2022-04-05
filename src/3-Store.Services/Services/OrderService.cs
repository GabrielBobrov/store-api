using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
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
    public class OrderService : IOrderServices
    {
        private readonly IMapper _mapper;
        private readonly ICostumerRepository _costumerRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMediatorHandler _mediator;

        public OrderService(
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

        public async Task<Optional<OrderDto>> CreateAsync(OrderDto orderDto)
        {
            Expression<Func<Costumer, bool>> filter = c
                => c.Id == orderDto.CostumerId;

            var costumerExists = await _costumerRepository.GetAsync(filter);

            if (costumerExists == null)
            {
                await _mediator.PublishDomainNotificationAsync(new DomainNotification(
                    ErrorMessages.CostumerNotFound,
                    DomainNotificationType.CostumerNotFound));

                return new Optional<OrderDto>();
            }

            var order = _mapper.Map<Order>(orderDto);

            order.setStatus(Status.Awaiting);
            order.SetCreatedAt(DateTime.UtcNow);
            order.Validate();

            if (!order.IsValid)
            {
                await _mediator.PublishDomainNotificationAsync(new DomainNotification(
                   ErrorMessages.OrderInvalid(order.ErrorsToString()),
                   DomainNotificationType.OrderInvalid));

                return new Optional<OrderDto>();
            }

            var orderCreated = await _orderRepository.CreateAsync(order);

            return _mapper.Map<OrderDto>(orderCreated);
        }

    }
}