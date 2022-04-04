using Store.Core.Communication.Mediator.Interfaces;
using Store.Core.Communication.Messages.Notifications;
using MediatR;
using System.Threading.Tasks;

namespace Store.Core.Communication.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishDomainNotificationAsync<T>(T appNotification)
            where T : DomainNotification
            => await _mediator.Publish(appNotification);
    }
}