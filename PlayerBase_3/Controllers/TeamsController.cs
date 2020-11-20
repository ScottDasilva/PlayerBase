using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlayerBase_3.Data;
using PlayerBase_3.Models;

namespace PlayerBase_3.Controllers
{
    [Authorize]
    public class TeamsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITeamRepository _teamRepository;
        private readonly IPlayerRepository _playerRepository;
        ApplicationUser user;
        Player player;

        public TeamsController(
            ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ITeamRepository teamRepository,
            IPlayerRepository playerRepository
            )
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _teamRepository = teamRepository;
            _playerRepository = playerRepository;
        }

        // GET: Teams
        public async Task<IActionResult> Index(string searchBy, string search)
        {
            user = _userManager.FindByEmailAsync(User.Identity.Name).Result;
            player = _playerRepository.GetPlayerByUserId(user.Id);
            ViewBag.Player = player;

            if (search != null)
            {
                if (searchBy == "Name")
                {
                    return View(await _context.Teams.Where(t => t.Name.Contains(search)).ToListAsync());
                }

                if (searchBy == "City")
                {
                    return View(await _context.Teams.Where(t => t.City.Contains(search)).ToListAsync());
                }

                if (searchBy == "Province")
                {
                    return View(await _context.Teams.Where(t => t.Province.Contains(search)).ToListAsync());
                }

                if (searchBy == "League")
                {
                    return View(await _context.Teams.Where(t => t.League.Contains(search)).ToListAsync());
                }
            }
            
            return View(await _context.Teams.ToListAsync());
        }

        // GET: Teams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            user = _userManager.FindByEmailAsync(User.Identity.Name).Result;
            player = _playerRepository.GetPlayerByUserId(user.Id);
            ViewBag.Player = player;

            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .FirstOrDefaultAsync(m => m.Id == id);
            if (team == null)
            {
                return NotFound();
            }

            ViewBag.Players = GetPlayersOnTeam(team.Id);

            return View(team);
        }

        // GET: Teams/Create
        public IActionResult Create()
        {
            user = _userManager.FindByEmailAsync(User.Identity.Name).Result;
            player = _playerRepository.GetPlayerByUserId(user.Id);
            ViewBag.Player = player;
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,City,Province,Name,League,Abbreviation,Email")] Team team)
        {
            user = _userManager.FindByEmailAsync(User.Identity.Name).Result;
            player = _playerRepository.GetPlayerByUserId(user.Id);
            ViewBag.Player = player;

            if (ModelState.IsValid)
            {
                var result = await _userManager.AddToRoleAsync(user, "Manager");
                if (result.Succeeded)
                {
                    _teamRepository.Add(team);
                    player.TeamId = team.Id;
                    _playerRepository.Update(player);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(team);
        }

        // GET: Teams/Edit/5
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            user = _userManager.FindByEmailAsync(User.Identity.Name).Result;
            player = _playerRepository.GetPlayerByUserId(user.Id);
            ViewBag.Player = player;

            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }
            return View(team);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,City,Province,Name,League,Abbreviation,Email")] Team team)
        {
            user = _userManager.FindByEmailAsync(User.Identity.Name).Result;
            player = _playerRepository.GetPlayerByUserId(user.Id);
            ViewBag.Player = player;
            if (id != team.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(team);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamExists(team.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(team);
        }

        // GET: Teams/Delete/5
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Delete(int? id)
        {
            user = _userManager.FindByEmailAsync(User.Identity.Name).Result;
            player = _playerRepository.GetPlayerByUserId(user.Id);
            ViewBag.Player = player;
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .FirstOrDefaultAsync(m => m.Id == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            user = _userManager.FindByEmailAsync(User.Identity.Name).Result;
            player = _playerRepository.GetPlayerByUserId(user.Id);
            ViewBag.Player = player;
            var team = await _context.Teams.FindAsync(id);
            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamExists(int id)
        {
            return _context.Teams.Any(e => e.Id == id);
        }

        private List<Player> GetPlayersOnTeam(int id)
        {
            return _teamRepository.GetPlayers(id);
        }
    }
}
