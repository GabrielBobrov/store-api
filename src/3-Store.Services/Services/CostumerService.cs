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
        private readonly IMediatorHandler _mediator;

        public CostumerServices(
            IMapper mapper,
            ICostumerRepository costumerRepository,
            IMediatorHandler mediator)
        {
            _mapper = mapper;
            _costumerRepository = costumerRepository;
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
            var allOperators = await _costumerRepository.GetAllAsync();
            var allOperatorsDto = _mapper.Map<IList<CostumerDto>>(allOperators);

            return new Optional<IList<CostumerDto>>(allOperatorsDto);
        }

        public async Task<Optional<CostumerDto>> GetAsync(long id)
        {
            var costumer = await _costumerRepository.GetAsync(id);
            var costumerDto = _mapper.Map<CostumerDto>(costumer);

            return new Optional<CostumerDto>(costumerDto);
        }
    }
}