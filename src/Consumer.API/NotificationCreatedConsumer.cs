using MassTransit;
using Shared.Interfaces;
using System.Text.Json;

namespace Consumer.API
{
    public class NotificationCreatedConsumer : IConsumer<INotificationCreated>
    {
        public async Task Consume(ConsumeContext<INotificationCreated> context)
        {
            var serializedMessage = JsonSerializer.Serialize(context.Message);

            Console.WriteLine($"NotificationCreated event consumed. Message: {serializedMessage}");
        }
    }
}
