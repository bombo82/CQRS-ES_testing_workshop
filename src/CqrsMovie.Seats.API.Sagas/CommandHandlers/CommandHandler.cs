using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Muflone.Core;
using Muflone.Messages.Commands;
using Muflone.Persistence;

namespace CqrsMovie.Seats.API.Sagas.CommandHandlers
{
  public abstract class CommandHandler<TCommand> : ICommandHandler<TCommand> where TCommand : ICommand
  {
    protected readonly IRepository Repository;
    protected readonly ILogger Logger;

    protected CommandHandler(IRepository repository, ILoggerFactory loggerFactory)
    {
      this.Repository = repository ?? throw new ArgumentNullException(nameof(repository));
      this.Logger = loggerFactory.CreateLogger(this.GetType());
    }

    protected async Task<TAggregate> Get<TAggregate>(IDomainId id) where TAggregate : AggregateRoot
    {
      var aggregate = await this.Repository.GetById<TAggregate>(id);
      if (aggregate == null)
        throw new Exception($"{typeof(TAggregate).Name} not found");
      return aggregate;
    }

    protected async Task Save<TAggregate>(TAggregate aggregate) where TAggregate : AggregateRoot
    {
      await this.Repository.Save(aggregate, Guid.NewGuid());
    }

    public abstract Task Handle(TCommand command);

    protected virtual void Dispose(bool disposing)
    {
      if (disposing)
      {
      }
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize(this);
    }

    ~CommandHandler()
    {
      this.Dispose(false);
    }
  }
}