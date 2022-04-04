using Store.API.ViewModels;
using Store.Core.Communication.Handlers;
using Store.Core.Communication.Messages.Notifications;
using Store.Core.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Store.API.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        private readonly DomainNotificationHandler _domainNotificationHandler;

        protected BaseController(
           INotificationHandler<DomainNotification> domainNotificationHandler)
        {
            _domainNotificationHandler = domainNotificationHandler as DomainNotificationHandler;
        }

        protected bool HasNotifications()
           => _domainNotificationHandler.HasNotifications();

        protected ObjectResult Created(dynamic responseObject)
            => StatusCode(201, responseObject);

        protected ObjectResult Result()
        {
            var notification = _domainNotificationHandler
                .Notifications
                .FirstOrDefault();

            return StatusCode(GetStatusCodeByNotificationType(notification.Type),
                new ResultViewModel
                {
                    Message = notification.Message,
                    Success = false,
                    Data = new { }
                });
        }

        private int GetStatusCodeByNotificationType(DomainNotificationType errorType)
        {
            return errorType switch
            {
                //Conflict
                DomainNotificationType.CostumerAlreadyExists
                    => 409,

                //Unprocessable Entity
                DomainNotificationType.CostumerInvalid
                    => 422,

                DomainNotificationType.InvalidEnum
                    => 422,

                //Not Found
                DomainNotificationType.CostumerNotFound
                    => 404,

                (_) => 500,
            };
        }
    }
}