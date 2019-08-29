using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using Clan.ObjectModel;
using Clan.Web.ViewModels;

namespace Clan.Web.Controllers
{
    public class RosterController : Controller
    {
        public IActionResult Index()
        {
            var viewModel = new RosterViewModel
            {
                Members = new List<User>
                {
                    new User
                    {
                        Name = "rhaydeo",
                        Email = "adam.stirtan@outlook.com",
                        Active = true,
                        BattleTag = "Rhaydeo#11799",
                        Created = DateTime.UtcNow,
                        Modified = DateTime.UtcNow
                    },
                    new User
                    {
                        Name = "lewzer",
                        Email = "dave@gmail.com",
                        Active = true,
                        BattleTag = "lewzer#11799",
                        Created = DateTime.UtcNow,
                        Modified = DateTime.UtcNow
                    }
                }
            };

            return View(viewModel);
        }
    }
}