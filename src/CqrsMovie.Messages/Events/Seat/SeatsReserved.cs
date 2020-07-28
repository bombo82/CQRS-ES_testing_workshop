using System.Collections.Generic;
using CqrsMovie.SharedKernel.Domain.Ids;
using Muflone.Messages.Events;

namespace CqrsMovie.Messages.Events.Seat
{
    public class SeatsReserved : DomainEvent
    {
        private DailyProgrammingId aggregateId;
        public IEnumerable<Dtos.Seat> Seats { get; }

        public SeatsReserved(DailyProgrammingId aggregateId, IEnumerable<Dtos.Seat> seats) : base(aggregateId)
        {
            this.aggregateId = aggregateId;
            this.Seats = seats;
        }
    }
}