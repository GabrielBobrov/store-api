using Store.Core.Communication.Messages.Notifications;
using System.Threading.Tasks;

namespace Store.Core.Communication.Mediator.Interfaces
{
    public interface IMediatorHandler
    {
        Task PublishDomainNotificationAsync<T>(T appNotification)
            where T : DomainNotification;
    }
}