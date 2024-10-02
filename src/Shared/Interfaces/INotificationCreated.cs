using Shared.Enums;

namespace Shared.Interfaces
{
    public interface INotificationCreated
    {
        DateTime Date { get; }
        string Message { get; }
        EnumNotificationType Type { get; }
    }
}
