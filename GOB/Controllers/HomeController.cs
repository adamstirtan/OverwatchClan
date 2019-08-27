using Microsoft.AspNetCore.Mvc;

using Clan.Web.ViewModels;

namespace Clan.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var viewModel = new HomeViewModel();

            return View(viewModel);
        }
    }
}