using System;
using System.Threading.Tasks;
using CqrsMovie.Messages.Commands.Seat;
using CqrsMovie.Seats.Domain.Entities;
using CqrsMovie.SharedKernel.Domain.Ids;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace CqrsMovie.Seats.API.Sagas.CommandHandlers
{
  public class BookSeatsCommandHandler : CommandHandler<BookSeats>
  {
    public BookSeatsCommandHandler(IRepository repository, ILoggerFactory loggerFactory) : base(repository, loggerFactory)
    {
    }

    public override async Task Handle(BookSeats command)
    {
      try
      {
        var entity = await this.Repository.GetById<DailyProgramming>(command.AggregateId);
        //entity.BookSeats((DailyProgrammingId)entity.Id, command.Seats);
        await this.Repository.Save(entity, Guid.NewGuid(), headers => { });
      }
      catch (Exception e)
      {
        this.Logger.LogError($"BookSeatsCommand: Error processing the command: {e.Message} - StackTrace: {e.StackTrace}");
        throw;
      }
    }
  }
}