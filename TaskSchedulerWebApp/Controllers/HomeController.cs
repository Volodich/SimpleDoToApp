using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskSchedulerWebApp.Settings;

namespace TaskSchedulerWebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public HomeController()
        {
            Config.SidebarVisible = true;
            Config.UseBootstrap = false;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contacts()
        {
            return View();
        }
        
    }
}
