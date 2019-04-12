using Microsoft.AspNetCore.Mvc;

namespace Tasks.API.Controllers
{
    public class HomeController : Controller
    {
        //GET
        public IActionResult Index()
        {
            return new RedirectResult("~/swagger");
        }
    }
}
