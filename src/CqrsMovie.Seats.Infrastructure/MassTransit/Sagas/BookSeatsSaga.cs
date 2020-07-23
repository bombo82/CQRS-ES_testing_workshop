using System.Threading.Tasks;
using CqrsMovie.Messages.Commands.Seat;
using Muflone;
using Muflone.Saga;
using Muflone.Saga.Persistence;

namespace CqrsMovie.Seats.Infrastructure.MassTransit.Sagas
{
    public class BookSeatsSaga : SagaHandler<StartBookSeatsSaga, BookSeatsSaga.MyData>
    {
        public class MyData
        {
            public string Value1;
            public string Value2;
        }

        public BookSeatsSaga(IServiceBus serviceBus, ISagaRepository repository) : base(serviceBus, repository)
        {
        }

        public override Task StartedBy(StartBookSeatsSaga command)
        {
            return Task.CompletedTask;
        }
    }
}