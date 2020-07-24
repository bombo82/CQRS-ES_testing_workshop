using System;
using System.Collections.Generic;
using Muflone.Core;
using Muflone.Messages.Commands;

namespace CqrsMovie.Messages.Commands.Seat
{
    public class StartBookSeatsSaga : Command
    {
        public IEnumerable<Dtos.Seat> Seats { get; }

        public StartBookSeatsSaga(IDomainId aggregateId, IEnumerable<Dtos.Seat> seats, Guid correlationId, string who = "anonymous")
            : base(aggregateId, correlationId, who)
        {
            Seats = seats;
        }
    }
}