using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Highsoft.Web.Mvc.Charts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlayerBase_3.Data;
using PlayerBase_3.Models;

namespace PlayerBase_3.Controllers
{
    public class PlayersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ITeamRepository _teamRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPlayerRepository _playerRepository;
        ApplicationUser user;
        Player player;

        public PlayersController(
            ApplicationDbContext context,
            ITeamRepository teamRepository,
            UserManager<ApplicationUser> userManager,
            IPlayerRepository playerRepository)
        {
            _context = context;
            _teamRepository = teamRepository;
            _userManager = userManager;
            _playerRepository = playerRepository;
        }

        // GET: Players
        public async Task<IActionResult> Index(string searchBy, string search)
        {
            user = _userManager.FindByEmailAsync(User.Identity.Name).Result;
            player = _playerRepository.GetPlayerByUserId(user.Id);
            ViewBag.Player = player;

            if (search != null)
            {
                if (searchBy == "Name")
                {
                    return View(await _context.Players.Where(p => p.FirstName.Contains(search) || p.LastName.Contains(search) || (p.FirstName + " " + p.LastName).Contains(search)).ToListAsync());
                }                              
                                               
                if (searchBy == "City")        
                {                              
                    return View(await _context.Players.Where(p => p.City.Contains(search)).ToListAsync());
                }                              
                                               
                if (searchBy == "Province")    
                {                              
                    return View(await _context.Players.Where(p => p.Province.Contains(search)).ToListAsync());
                }                              
                                               
                if (searchBy == "League")      
                {                              
                    return View(await _context.Players.Where(p => p.Position.Contains(search)).ToListAsync());
                }
            }

            return View(await _context.Players.ToListAsync());
        }

        // GET: Players/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            user = _userManager.FindByEmailAsync(User.Identity.Name).Result;
            player = _playerRepository.GetPlayerByUserId(user.Id);
            ViewBag.Player = player;
            if (id == null)
            {
                return NotFound();
            }

            player = await _context.Players
                .FirstOrDefaultAsync(m => m.Id == id);
            if (player == null)
            {
                return NotFound();
            }

            double ggp = Convert.ToDouble(player.Goals) / Convert.ToDouble(player.GamesPlayed);
            double agp = Convert.ToDouble(player.Assists) / Convert.ToDouble(player.GamesPlayed);
            double pimgp = Convert.ToDouble(player.PenaltyMinutes) / Convert.ToDouble(player.GamesPlayed);
            double pgp = (Convert.ToDouble(player.Goals) + Convert.ToDouble(player.Assists)) / Convert.ToDouble(player.GamesPlayed);

            List<double> playerValues = new List<double> { Math.Round(ggp, 2), Math.Round(agp, 2), Math.Round(pimgp, 2), Math.Round(pgp, 2) };
            List<ColumnSeriesData> playerData = new List<ColumnSeriesData>();

            playerValues.ForEach(p => playerData.Add(new ColumnSeriesData { Y = p }));
            ViewData["playerData"] = playerData;

            if (player.TeamId != null)
            {
                ViewBag.playerTeam = _teamRepository.GetTeam(Convert.ToInt32(player.TeamId));
            } else
            {
                ViewBag.playerTeam = "N/A";
            }

            return View(player);
        }

        // GET: Players/Create
        public IActionResult Create()
        {
            user = _userManager.FindByEmailAsync(User.Identity.Name).Result;
            player = _playerRepository.GetPlayerByUserId(user.Id);
            ViewBag.Player = player;
            return View();
        }

        // POST: Players/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,BirthDate,Position,Province,City,TeamId,Email,GamesPlayed,Goals,Assists,PenaltyMinutes")] Player player)
        {
            user = _userManager.FindByEmailAsync(User.Identity.Name).Result;
            player = _playerRepository.GetPlayerByUserId(user.Id);
            ViewBag.Player = player;
            if (ModelState.IsValid)
            {
                _context.Add(player);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(player);
        }

        // GET: Players/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            user = _userManager.FindByEmailAsync(User.Identity.Name).Result;
            player = _playerRepository.GetPlayerByUserId(user.Id);
            ViewBag.Player = player;

            if (id == null)
            {
                return NotFound();
            }

            player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }
            return View(player);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,BirthDate,Position,Province,City,TeamId,Email,GamesPlayed,Goals,Assists,PenaltyMinutes")] Player player)
        {
            user = _userManager.FindByEmailAsync(User.Identity.Name).Result;
            player.UserId = user.Id;
            ViewBag.Player = player;

            if (id != player.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(player);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerExists(player.Id))
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
            return View(player);
        }

        // GET: Players/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            user = _userManager.FindByEmailAsync(User.Identity.Name).Result;
            player = _playerRepository.GetPlayerByUserId(user.Id);
            ViewBag.Player = player;
            if (id == null)
            {
                return NotFound();
            }

            player = await _context.Players
                .FirstOrDefaultAsync(m => m.Id == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            user = _userManager.FindByEmailAsync(User.Identity.Name).Result;
            player = _playerRepository.GetPlayerByUserId(user.Id);
            ViewBag.Player = player;
            player = await _context.Players.FindAsync(id);
            _context.Players.Remove(player);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerExists(int id)
        {
            return _context.Players.Any(e => e.Id == id);
        }
    }
}
