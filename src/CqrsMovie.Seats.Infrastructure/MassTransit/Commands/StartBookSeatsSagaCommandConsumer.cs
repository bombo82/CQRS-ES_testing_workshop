using System.Threading.Tasks;
using CqrsMovie.Messages.Commands.Seat;
using MassTransit;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;

namespace CqrsMovie.Seats.Infrastructure.MassTransit.Commands
{
    public class StartBookSeatsSagaCommandConsumer : CommandConsumer<StartBookSeatsSaga>
    {
        public StartBookSeatsSagaCommandConsumer(IRepository repository, ILoggerFactory loggerFactory) : base(repository, loggerFactory)
        {
        }

        protected override ICommandHandler<StartBookSeatsSaga> Handler { get; }
        public override Task Consume(ConsumeContext<StartBookSeatsSaga> context)
        {
            return Task.CompletedTask;
        }
    }
}