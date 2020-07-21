using System.Threading.Tasks;
using CqrsMovie.Messages.Commands.Seat;
using MassTransit;
using Muflone;
using Muflone.MassTransit.RabbitMQ.Consumers;
using Muflone.Saga;
using Muflone.Saga.Persistence;

namespace CqrsMovie.Seats.API.Sagas.Consumers
{
    public class StartBookSeatsSagaConsumer : SagaStartedByConsumerBase<StartBookSeatsSaga>
    {
        private readonly ISagaRepository repository;
        private readonly IServiceBus serviceBus;

        public StartBookSeatsSagaConsumer(ISagaRepository repository, IServiceBus serviceBus)
        {
            this.repository = repository;
            this.serviceBus = serviceBus;
        }

        public override async Task Consume(ConsumeContext<StartBookSeatsSaga> context)
        {
            using var handler = this.Handler;
            await handler.StartedBy(context.Message);
        }

        protected override ISagaStartedBy<StartBookSeatsSaga> Handler => new BookSeatsSaga(this.serviceBus, this.repository);
    }
}