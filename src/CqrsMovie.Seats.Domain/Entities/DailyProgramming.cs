using System;
using System.Collections.Generic;
using System.Linq;
using CqrsMovie.Core.Enums;
using CqrsMovie.Messages.Events.Seat;
using CqrsMovie.SharedKernel.Domain.Ids;
using Muflone.Core;

namespace CqrsMovie.Seats.Domain.Entities
{
    public class DailyProgramming : AggregateRoot
    {
        private MovieId movieId;
        private ScreenId screenId;
        private DateTime date;
        private IList<Seat> seats;

        //TODO: Implement user information (due to online shopping)
        //private Guid userId;

        protected DailyProgramming()
        { }

        public static DailyProgramming CreateDailyProgramming(DailyProgrammingId aggregateId, MovieId movieId,
            ScreenId screenId, DateTime date, IEnumerable<Messages.Dtos.Seat> freeSeats, string movieTitle,
            string screenName)
        {
            return new DailyProgramming(aggregateId, movieId, screenId, date, freeSeats, movieTitle, screenName);
        }

        private DailyProgramming(DailyProgrammingId aggregateId, MovieId movieId, ScreenId screenId, DateTime date, IEnumerable<Messages.Dtos.Seat> freeSeats, string movieTitle, string screenName)
        {
            //Null checks etc. ....


            RaiseEvent(new DailyProgrammingCreated(aggregateId, movieId, screenId, date, freeSeats, movieTitle, screenName));
        }

        private void Apply(DailyProgrammingCreated @event)
        {
            Id = @event.AggregateId;
            movieId = @event.MovieId;
            screenId = @event.ScreenId;
            date = @event.Date;
            seats = @event.Seats.ToEntity(SeatState.Free);
        }

        public void Reserve(IEnumerable<Messages.Dtos.Seat> seatsToReserve)
        {
            var seatsToCheck = seats.Intersect(seatsToReserve.ToEntity(SeatState.Free));
            if (seatsToCheck.Count() == seatsToReserve.Count())
            {
                RaiseEvent(new SeatsReserved(new DailyProgrammingId(Id.Value), seatsToReserve));
            }
            else
            {
                throw new Exception("booom");
            }
        }

        private void Apply(SeatsReserved @event)
        {
            foreach (var seatReserved in @event.Seats)
            {
                var seat = seats.FirstOrDefault(s => s.Equals(seatReserved.ToEntity(SeatState.Free)));
                seats.Remove(seat);
                seats.Add(new Seat(seat.Row, seat.Number, SeatState.Reserved));
            }

        }
    }
}
