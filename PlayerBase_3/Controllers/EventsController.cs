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
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEventRepository _eventRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        ApplicationUser user;
        Player player;

        public EventsController(ApplicationDbContext context,
            IEventRepository eventRepository,
            IPlayerRepository playerRepository,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _eventRepository = eventRepository;
            _playerRepository = playerRepository;
            _userManager = userManager;
        }

        // GET: Events
        public async Task<IActionResult> Index()
        {
            user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            player = _playerRepository.GetPlayerByUserId(user.Id);
            ViewBag.Player = player;

            return View(_eventRepository.GetTeamEvents(Convert.ToInt32(player.TeamId)).ToList());
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            player = _playerRepository.GetPlayerByUserId(user.Id);
            ViewBag.Player = player;

            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Events/Create
        [Authorize(Roles = "Manager")]
        public IActionResult Create()
        {
            user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            player = _playerRepository.GetPlayerByUserId(user.Id);
            ViewBag.Player = player;
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Create([Bind("Id,Title,StartTime,EndTime,Location,Date,Description")] Event @event)
        {
            user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            player = _playerRepository.GetPlayerByUserId(user.Id);
            ViewBag.Player = player;
            if (ModelState.IsValid)
            {
                @event.TeamId = Convert.ToInt32(player.TeamId);
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@event);
        }

        // GET: Events/Edit/5
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            player = _playerRepository.GetPlayerByUserId(user.Id);
            ViewBag.Player = player;
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TeamId,Title,StartTime,EndTime,Location,Date,Description")] Event @event)
        {
            user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            player = _playerRepository.GetPlayerByUserId(user.Id);
            ViewBag.Player = player;
            if (id != @event.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.Id))
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
            return View(@event);
        }

        // GET: Events/Delete/5
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Delete(int? id)
        {
            user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            player = _playerRepository.GetPlayerByUserId(user.Id);
            ViewBag.Player = player;
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            player = _playerRepository.GetPlayerByUserId(user.Id);
            ViewBag.Player = player;
            var @event = await _context.Events.FindAsync(id);
            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }
    }
}
