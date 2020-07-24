using System;
using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.MassTransit.RabbitMQ.Consumers;
using Muflone.Messages.Commands;
using Muflone.Saga.Persistence;

namespace CqrsMovie.Seats.Infrastructure.MassTransit.Sagas
{
    public abstract class SagaConsumer<TCommand> : SagaStartedByConsumerBase<TCommand> where TCommand : Command
    {
        protected readonly ISagaRepository Repository;
        protected readonly IServiceBus ServiceBus;
        protected readonly ILoggerFactory LoggerFactory;
        protected readonly ILogger Logger;

        protected SagaConsumer(ISagaRepository repository, IServiceBus serviceBus,
            ILoggerFactory loggerFactory)
        {
            this.Repository = repository;
            this.ServiceBus = serviceBus;
            this.LoggerFactory = loggerFactory;
            this.Logger = loggerFactory.CreateLogger(this.GetType());
        }
    }
}