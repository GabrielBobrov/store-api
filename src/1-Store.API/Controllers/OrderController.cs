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
    [Route("/api/v1/orders")]
    public class OrderController : BaseController
    {

        private readonly IMapper _mapper;
        private readonly IOrderServices _orderService;

        public OrderController(
            IMapper mapper,
            IOrderServices orderService,
            INotificationHandler<DomainNotification> domainNotificationHandler)
            : base(domainNotificationHandler)
        {
            _mapper = mapper;
            _orderService = orderService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateOrderViewModel orderViewModel)
        {
            var orderDto = _mapper.Map<OrderDto>(orderViewModel);
            var orderCreated = await _orderService.CreateAsync(orderDto);

            if (HasNotifications())
                return Result();

            return Created(new ResultViewModel
            {
                Message = "Ordem criada com sucesso!",
                Success = true,
                Data = orderCreated.Value
            });
        }
    }
}