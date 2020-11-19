using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using PlayerBase_3.Models;

namespace PlayerBase_3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPlayerRepository _playerRepository;
        ApplicationUser user;
        Player player;

        public HomeController(
            ILogger<HomeController> logger,
            UserManager<ApplicationUser> userManager,
            IPlayerRepository playerRepository)
        {
            _logger = logger;
            _userManager = userManager;
            _playerRepository = playerRepository;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                user = _userManager.FindByEmailAsync(User.Identity.Name).Result;
                player = _playerRepository.GetPlayerByUserId(user.Id);
                ViewBag.Player = player;
                return View();
            }
            return View();
        }

        public IActionResult Privacy()
        {
            user = _userManager.FindByEmailAsync(User.Identity.Name).Result;
            player = _playerRepository.GetPlayerByUserId(user.Id);
            ViewBag.Player = player;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
