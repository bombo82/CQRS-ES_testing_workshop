using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CqrsMovie.Seats.API.Sagas.Controllers.v1
{
  public class HomeController : BaseController
  {
    public HomeController(ILoggerFactory loggerFactory) : base(loggerFactory)
    {
    }

    public ActionResult Index()
    {
      return this.View();
    }

    public IActionResult Get()
    {
      return this.Ok();
    }
  }
}