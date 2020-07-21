using System.IO;
using System.Threading;
using CqrsMovie.Seats.Sagas.Consumers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Muflone.MassTransit.RabbitMQ;

namespace CqrsMovie.Seats.Sagas
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            services.Configure<ServiceBusOptions>(x => configuration.GetSection("MassTransit:RabbitMQ"));
            var serviceBusOptions = new ServiceBusOptions();
            configuration.GetSection("MassTransit:RabbitMQ").Bind(serviceBusOptions);

            services.AddMufloneMassTransitWithRabbitMQ(serviceBusOptions, x =>
            {
                x.AddConsumer<StartBookSeatsSagaConsumer>();
            });

            // wait - not to end
            new AutoResetEvent(false).WaitOne();
        }
    }
}
