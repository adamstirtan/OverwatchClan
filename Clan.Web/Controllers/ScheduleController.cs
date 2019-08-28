using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Clan.Web.ViewModels;

namespace Clan.Web.Controllers
{
    [Authorize]
    public class ScheduleController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new ScheduleViewModel();

            return View(viewModel);
        }
    }
}