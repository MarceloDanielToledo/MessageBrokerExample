using Shared.Enums;

namespace Shared.DTOs
{
    public class NotificationDTO
    {
        public DateTime Date { get; }
        public string Message { get; }
        public EnumNotificationType Type { get; }
    }
}
