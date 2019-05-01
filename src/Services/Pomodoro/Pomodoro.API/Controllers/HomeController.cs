using Microsoft.AspNetCore.Mvc;

namespace Athena.Pomodoro.API.Controllers
{
    public class HomeController : Controller
    {
        //GET: /<controller>/
        public IActionResult Index()
        {
            return new RedirectResult("~/swagger");
        }
    }
}
