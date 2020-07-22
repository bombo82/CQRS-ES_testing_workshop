using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CqrsMovie.Seats.API.Sagas.Controllers
{
  public abstract class BaseController: Controller
  {
    protected ILogger Logger { get; }

    protected BaseController(ILoggerFactory loggerFactory)
    {
      this.Logger = loggerFactory.CreateLogger(this.GetType());
    }
  }
}
