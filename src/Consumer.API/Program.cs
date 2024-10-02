
using MassTransit;
using System.Reflection;

namespace Consumer.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMassTransit(busConfigurator =>
            {
                var entryAssembly = Assembly.GetExecutingAssembly();
                busConfigurator.AddConsumers(entryAssembly);
                busConfigurator.UsingRabbitMq((context, busFactoryConfigurator) =>
                {
                    busFactoryConfigurator.Host("rabbitmq", "/", h => {  });
                    busFactoryConfigurator.ConfigureEndpoints(context);
                });
            });
            var app = builder.Build();
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.Run();
        }
    }
}
