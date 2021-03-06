using Store.Core.Enums;
using Store.Core.Enums;

namespace Store.Core.Communication.Messages.Notifications
{
    public class DomainNotification : Notification
    {
        public string Message { get; private set; }
        public DomainNotificationType Type { get; private set; }

        public DomainNotification(string message, DomainNotificationType type)
        {
            Message = message;
            Type = type;
        }
    }
}