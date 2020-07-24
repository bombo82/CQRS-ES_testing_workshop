using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.MassTransit.RabbitMQ.Consumers;
using Muflone.Messages.Events;
using Muflone.Saga.Persistence;

namespace CqrsMovie.Seats.Infrastructure.MassTransit.Sagas
{
    public abstract class SagaEventConsumer<TEvent> : SagaEventConsumerBase<TEvent> where TEvent : Event
    {
        protected readonly ISagaRepository Repository;
        protected readonly IServiceBus ServiceBus;
        protected readonly ILoggerFactory LoggerFactory;
        protected readonly ILogger Logger;

        protected SagaEventConsumer(ISagaRepository repository, IServiceBus serviceBus,
            ILoggerFactory loggerFactory)
        {
            this.Repository = repository;
            this.ServiceBus = serviceBus;
            this.LoggerFactory = loggerFactory;
            this.Logger = loggerFactory.CreateLogger(this.GetType());
        }
    }
}