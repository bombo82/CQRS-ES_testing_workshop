using System.Collections.Generic;
using CqrsMovie.SharedKernel.Domain.Ids;
using Muflone.Messages.Commands;

namespace CqrsMovie.Messages.Commands.Seat
{
    public class ReserveSeats : Command
    {
        public IEnumerable<Dtos.Seat> Seats { get; private set; }

        public ReserveSeats(DailyProgrammingId aggregateId, IEnumerable<Dtos.Seat> seats) : base(aggregateId)
        {
            this.Seats = seats;
        }
    }
}