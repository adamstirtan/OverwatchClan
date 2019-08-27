using Microsoft.AspNetCore.Mvc;

using GOB.ViewModels;

namespace GOB.Controllers
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