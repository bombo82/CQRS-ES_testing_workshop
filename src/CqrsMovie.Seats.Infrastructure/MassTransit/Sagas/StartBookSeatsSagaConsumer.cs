using System;
using System.Threading.Tasks;
using CqrsMovie.Messages.Commands.Seat;
using MassTransit;
using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Saga;
using Muflone.Saga.Persistence;

namespace CqrsMovie.Seats.Infrastructure.MassTransit.Sagas
{
    public class StartBookSeatsSagaConsumer : SagaStartedByConsumer<StartBookSeatsSaga>
    {
        public StartBookSeatsSagaConsumer(ISagaRepository repository, IServiceBus serviceBus, ILoggerFactory loggerFactory)
            : base(repository, serviceBus, loggerFactory)
        {
        }

        public override async Task Consume(ConsumeContext<StartBookSeatsSaga> context)
        {
            using(var handler = this.Handler)
                await handler.StartedBy(context.Message);
        }

        protected override ISagaStartedBy<StartBookSeatsSaga> Handler => new BookSeatsSaga(this.ServiceBus, this.Repository);
        
        #region Dispose
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~StartBookSeatsSagaConsumer()
        {
            Dispose(false);
        }

        #endregion
    }
}