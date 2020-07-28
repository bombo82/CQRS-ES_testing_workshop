using System;
using System.Threading.Tasks;
using CqrsMovie.Messages.Commands.Seat;
using CqrsMovie.Seats.Domain.Entities;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;

namespace CqrsMovie.Seats.Domain
{
    internal class ReserveSeatsCommandHandler : CommandHandler<ReserveSeats>
    {
        public ReserveSeatsCommandHandler(IRepository repository, ILoggerFactory loggerFactory) : base(repository, loggerFactory)
        {

        }

        public override async Task Handle(ReserveSeats command)
        {
            var aggregate = await Repository.GetById<DailyProgramming>(command.AggregateId);
            aggregate.Reserve(command.Seats);
            await Repository.Save(aggregate, Guid.NewGuid());
        }
    }
}