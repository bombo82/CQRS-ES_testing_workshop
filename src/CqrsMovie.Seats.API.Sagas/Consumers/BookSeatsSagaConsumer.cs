using System;
using System.Threading.Tasks;
using CqrsMovie.Messages.Commands.Seat;
using CqrsMovie.Seats.API.Sagas.CommandHandlers;
using CqrsMovie.Seats.Infrastructure.MassTransit.Commands;
using MassTransit;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;

namespace CqrsMovie.Seats.API.Sagas.Consumers
{
    public class BookSeatsSagaConsumer : CommandConsumer<StartBookSeatsSaga>
    {
        public BookSeatsSagaConsumer(ILoggerFactory loggerFactory) : base(null, loggerFactory)
        {
        }

        protected override ICommandHandler<StartBookSeatsSaga> Handler => new StartBookSeatsCommandHandler(this.LoggerFactory);
        public override async Task Consume(ConsumeContext<StartBookSeatsSaga> context)
        {
            try
            {
                using var handler = this.Handler;
                await handler.Handle(context.Message);
            }
            catch (Exception ex)
            {
                this.Logger.LogError($"StackTrace: {ex.StackTrace} - Source: {ex.Source} - Message: {ex.Message}");
            }
        }
    }
}