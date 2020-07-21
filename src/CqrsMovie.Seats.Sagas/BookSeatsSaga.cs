using System.Threading.Tasks;
using CqrsMovie.Messages.Commands.Seat;
using Muflone;
using Muflone.Saga;
using Muflone.Saga.Persistence;

namespace CqrsMovie.Seats.Sagas
{
    public class BookSeatsSaga : Saga<BookSeatsSaga.MyData>,
        ISagaStartedBy<StartBookSeatsSaga>
    {
        public class MyData
        {
            public string Value1;
            public string Value2;
        }

        public BookSeatsSaga(IServiceBus serviceBus, ISagaRepository repository) : base(serviceBus, repository)
        {
        }

        public Task StartedBy(StartBookSeatsSaga command)
        {
            return Task.CompletedTask;
        }
    }
}