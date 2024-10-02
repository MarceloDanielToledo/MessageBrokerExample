using MassTransit;
using MassTransit.Transports;
using Microsoft.AspNetCore.Http.HttpResults;
using Shared.DTOs;
using Shared.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();
    busConfigurator.UsingRabbitMq((context, busFactoryConfigurator) =>
    {
        busFactoryConfigurator.Host("rabbitmq", hostConfigurator => { });
    });
});
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapPost("/notification", async (NotificationDTO notification, IPublishEndpoint publishEndpoint) =>{
    await publishEndpoint.Publish<INotificationCreated>(new
    {
        NotificationDate = notification.Date,
        NotificationMessage = notification.Message,
        NotificationType = notification.Type
    });
    return Results.Ok();
}).WithOpenApi();

app.Run();

