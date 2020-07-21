using System;
using Muflone.Core;
using Muflone.Messages.Commands;

namespace CqrsMovie.Messages.Commands.Seat
{
    public class StartBookSeatsSaga : Command
    {
        public StartBookSeatsSaga(IDomainId aggregateId, Guid correlationId, string who = "anonymous") : base(aggregateId, correlationId, who)
        {
        }
    }
}