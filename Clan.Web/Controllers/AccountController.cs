using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

using Clan.ObjectModel;
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

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (string.IsNullOrEmpty(model.Email))
            {
                return Json(new ResponseViewModel(HttpStatusCode.BadRequest));
            }

            if (string.IsNullOrEmpty(model.Password))
            {
                return Json(new ResponseViewModel(HttpStatusCode.BadRequest));
            }

            var user = _userRepository
                .Where(x => x.Email == model.Email)
                .SingleOrDefault();

            if (user == null)
            {
                return Json(new ResponseViewModel(HttpStatusCode.Unauthorized));
            }

            if (user.LockedOut)
            {
                return Json(new ResponseViewModel(HttpStatusCode.Forbidden));
            }

            if (PasswordHasher.Verify(model.Password, user.PasswordHash))
            {
                user.LastLogin = DateTime.UtcNow;
                user.FailedLoginAttempts = 0;

                _userRepository.Update(user);

                await SetCookieAsync(user.Email, model.RememberMe);

                return Json(new ResponseViewModel(HttpStatusCode.OK)
                {
                    Redirect = string.IsNullOrEmpty(model.ReturnUrl) ? Url.Action("Index", "Schedule") : model.ReturnUrl
                });
            }
            else
            {
                user.FailedLoginAttempts++;

                if (user.FailedLoginAttempts >= 5)
                {
                    user.LockedOut = true;
                }

                _userRepository.Update(user);

                return Json(new ResponseViewModel(HttpStatusCode.Unauthorized));
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            var viewModel = new RegisterViewModel();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            var existingUser = _userRepository
                .Where(x => x.Email == model.Email)
                .SingleOrDefault();

            if (existingUser != null)
            {
                return Json(new ResponseViewModel(HttpStatusCode.Conflict));
            }

            var utcNow = DateTime.UtcNow;

            var user = new User
            {
                Email = model.Email,
                PasswordHash = PasswordHasher.Hash(model.Password),
                LastLogin = DateTime.UtcNow,
                LockedOut = false,
                FailedLoginAttempts = 0,
                Created = utcNow,
                Modified = utcNow
            };

            user = _userRepository.Create(user);

            await SetCookieAsync(user.Email, true);

            return Json(new ResponseViewModel(HttpStatusCode.OK)
            {
                Redirect = Url.Action("Index", "Schedule")
            });
        }

        [HttpGet]
        public async Task<IActionResult> LogOff()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Locked()
        {
            var viewModel = new LockedViewModel();

            return View(viewModel);
        }

        private async Task SetCookieAsync(string email, bool isPersistent)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, email)
            };

            ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");
            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

            await HttpContext.SignInAsync(principal, new AuthenticationProperties
            {
                IsPersistent = isPersistent
            });
        }
    }
}