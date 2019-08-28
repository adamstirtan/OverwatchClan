using System;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

using Clan.Web.Database.Repositories;
using Clan.Web.Security;
using Clan.Web.ViewModels;

namespace Clan.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

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

        [HttpPost]
        public IActionResult Login([FromBody] LoginViewModel model)
        {
            if (string.IsNullOrEmpty(model.Email))
            {
                return new StatusCodeResult(400);
            }

            if (string.IsNullOrEmpty(model.Password))
            {
                return new StatusCodeResult(400);
            }

            var user = _userRepository
                .Where(x => x.Email == model.Email)
                .SingleOrDefault();

            if (user == null)
            {
                return new StatusCodeResult(403);
            }

            if (PasswordHasher.Verify(model.Password, user.PasswordHash))
            {
                user.LastLogin = DateTime.UtcNow;
                _userRepository.Update(user);

                return new StatusCodeResult(200);
            }
            else
            {
                return new StatusCodeResult(403);
            }
        }
    }
}