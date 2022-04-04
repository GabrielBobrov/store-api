using System.Threading.Tasks;
using Store.API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Store.Services.Interfaces;
using AutoMapper;
using Store.Services.DTO;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using Store.Core.Communication.Messages.Notifications;
using Store.API.Messages;
using Store.Core.Enums;

namespace Store.API.Controllers
{

    [ApiController]
    [Authorize]
    [Route("/api/v1/costumers")]
    public class CostumerController : BaseController
    {

        private readonly IMapper _mapper;
        private readonly ICostumerServices _costumerService;

        public CostumerController(
            IMapper mapper,
            ICostumerServices costumerService,
            INotificationHandler<DomainNotification> domainNotificationHandler)
            : base(domainNotificationHandler)
        {
            _mapper = mapper;
            _costumerService = costumerService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAsync(long id)
        {
            var op = await _costumerService.GetAsync(id);

            if (HasNotifications())
                return Result();

            return Ok(new ResultViewModel
            {
                Message = ResponseMessages.SuccessMessageGetCostumer,
                Success = true,
                Data = op.Value
            });
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCostumerViewModel costumerViewModel)
        {
            var costumerDto = _mapper.Map<CostumerDto>(costumerViewModel);
            var costumerCreated = await _costumerService.CreateAsync(costumerDto);

            if (HasNotifications())
                return Result();

            return Created(new ResultViewModel
            {
                Message = "Usuário criado com sucesso!",
                Success = true,
                Data = costumerCreated.Value
            });
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var allCostumers = await _costumerService.GetAllAsync();

            if (HasNotifications())
                return Result();

            return Ok(new ResultViewModel
            {
                Message = ResponseMessages.SuccessMessageGetAllOCostumers,
                Success = true,
                Data = allCostumers.Value
            });
        }
    }
}