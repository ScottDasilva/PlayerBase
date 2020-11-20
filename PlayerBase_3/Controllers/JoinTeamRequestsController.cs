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
    public class JoinTeamRequestsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJoinTeamRequestRepository _joinTeamRequestRepository;
        private readonly IPlayerRepository _playerRepository;
        ApplicationUser user;
        Player player;

        public JoinTeamRequestsController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IPlayerRepository playerRepository,
            IJoinTeamRequestRepository joinTeamRequestRepository)
        {
            _context = context;
            _userManager = userManager;
            _joinTeamRequestRepository = joinTeamRequestRepository;
            _playerRepository = playerRepository;
        }

        public IActionResult ApplyToTeam(int teamId)
        {
            user = _userManager.FindByEmailAsync(User.Identity.Name).Result;
            player = _playerRepository.GetPlayerByUserId(user.Id);
            ViewBag.Player = player;

            JoinTeamRequest joinTeamRequest = new JoinTeamRequest
            {
                TeamId = teamId,
                PlayerId = player.Id
            };

            _joinTeamRequestRepository.Add(joinTeamRequest);

            return RedirectToAction("Index", "Teams");
        }

        [Authorize(Roles = "Manager")]
        public IActionResult ApprovePlayer(int playerId)
        {
            var joinTeamRequest = _joinTeamRequestRepository.GetJoinTeamRequestByPlayerId(playerId);
            var requestingPlayer = _playerRepository.GetPlayer(playerId);

            requestingPlayer.TeamId = joinTeamRequest.TeamId;

            _playerRepository.Update(requestingPlayer);
            _joinTeamRequestRepository.Delete(joinTeamRequest.Id);

            return RedirectToAction(nameof(Index));
        }

        // GET: JoinTeamRequests
        [Authorize(Roles = "Manager")]
        public IActionResult Index()
        {
            user = _userManager.FindByEmailAsync(User.Identity.Name).Result;
            player = _playerRepository.GetPlayerByUserId(user.Id);
            ViewBag.Player = player;

            List<JoinTeamRequest> joinTeamRequests = _context.JoinTeamRequest.Where(jr => jr.TeamId == player.TeamId).ToList();
            List<Player> players = new List<Player>();

            foreach (var request in joinTeamRequests)
            {
                var requestingPlayer = _playerRepository.GetPlayer(request.PlayerId);
                players.Add(requestingPlayer);
            }

            return View(players);
        }

        // GET: JoinTeamRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var joinTeamRequest = await _context.JoinTeamRequest
                .FirstOrDefaultAsync(m => m.Id == id);
            if (joinTeamRequest == null)
            {
                return NotFound();
            }

            return View(joinTeamRequest);
        }

        // GET: JoinTeamRequests/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JoinTeamRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PlayerId,TeamId")] JoinTeamRequest joinTeamRequest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(joinTeamRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(joinTeamRequest);
        }

        // GET: JoinTeamRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var joinTeamRequest = await _context.JoinTeamRequest.FindAsync(id);
            if (joinTeamRequest == null)
            {
                return NotFound();
            }
            return View(joinTeamRequest);
        }

        // POST: JoinTeamRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PlayerId,TeamId")] JoinTeamRequest joinTeamRequest)
        {
            if (id != joinTeamRequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(joinTeamRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JoinTeamRequestExists(joinTeamRequest.Id))
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
            return View(joinTeamRequest);
        }

        // GET: JoinTeamRequests/Delete/5
        [Authorize(Roles = "Manager")]
        public IActionResult Delete(int? id)
        {
            user = _userManager.FindByEmailAsync(User.Identity.Name).Result;
            player = _playerRepository.GetPlayerByUserId(user.Id);
            ViewBag.Player = player;

            if (id == null)
            {
                return NotFound();
            }

            var joinTeamRequest = _joinTeamRequestRepository.GetJoinTeamRequestByPlayerId(Convert.ToInt32(id));

            
            if (joinTeamRequest == null)
            {
                return NotFound();
            }

            return View(joinTeamRequest);
        }

        // POST: JoinTeamRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var joinTeamRequest = _joinTeamRequestRepository.GetJoinTeamRequestByPlayerId(id);
            _joinTeamRequestRepository.Delete(joinTeamRequest.Id);

            return RedirectToAction(nameof(Index));
        }

        private bool JoinTeamRequestExists(int id)
        {
            return _context.JoinTeamRequest.Any(e => e.Id == id);
        }
    }
}
