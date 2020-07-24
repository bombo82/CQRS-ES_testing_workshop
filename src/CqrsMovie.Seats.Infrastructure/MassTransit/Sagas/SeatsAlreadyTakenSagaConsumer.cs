using System.Threading.Tasks;
using CqrsMovie.Messages.Events.Seat;
using MassTransit;
using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Saga;
using Muflone.Saga.Persistence;

namespace CqrsMovie.Seats.Infrastructure.MassTransit.Sagas
{
    public class SeatsAlreadyTakenSagaConsumer : SagaEventConsumer<SeatsAlreadyTaken>
    {
        public SeatsAlreadyTakenSagaConsumer(ISagaRepository repository, IServiceBus serviceBus, ILoggerFactory loggerFactory)
            : base(repository, serviceBus, loggerFactory)
        {
        }

        protected override ISagaEventHandler<SeatsAlreadyTaken> Handler { get; }
        public override Task Consume(ConsumeContext<SeatsAlreadyTaken> context)
        {
            if (context.CorrelationId != null)
            {
                var sagaState = this.Repository.GetById<BookSeatsSaga.SagaBookedState>(context.CorrelationId.Value);

            }

            return Task.CompletedTask;

            //using (var handler = this.Handler)
            //    await handler.Handle(context.Message);
        }
    }
}