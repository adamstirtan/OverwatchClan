using Microsoft.AspNetCore.Mvc;

using Clan.Web.ViewModels;

namespace Clan.Web.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            var viewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                RememberMe = true
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Register()
        {
            var viewModel = new RegisterViewModel();

            return View(viewModel);
        }
    }
}