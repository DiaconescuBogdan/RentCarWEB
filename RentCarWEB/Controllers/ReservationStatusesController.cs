using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentCarWEB.Models;

namespace RentCarWEB.Controllers
{
    [Authorize(Roles = "admin, manager")]
    public class ReservationStatusesController : Controller
    {
        private readonly CarRentalContext _context;

        public ReservationStatusesController(CarRentalContext context)
        {
            _context = context;
        }

        // GET: ReservationStatuses
        public async Task<IActionResult> Index()
        {
            return View(await _context.ReservationStatuses.ToListAsync());
        }

        // GET: ReservationStatuses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservationStatus = await _context.ReservationStatuses
                .FirstOrDefaultAsync(m => m.ReservStatsID == id);
            if (reservationStatus == null)
            {
                return NotFound();
            }

            return View(reservationStatus);
        }

        // GET: ReservationStatuses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ReservationStatuses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReservStatsID,Name,Description")] ReservationStatus reservationStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservationStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reservationStatus);
        }

        // GET: ReservationStatuses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservationStatus = await _context.ReservationStatuses.FindAsync(id);
            if (reservationStatus == null)
            {
                return NotFound();
            }
            return View(reservationStatus);
        }

        // POST: ReservationStatuses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReservStatsID,Name,Description")] ReservationStatus reservationStatus)
        {
            if (id != reservationStatus.ReservStatsID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservationStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationStatusExists(reservationStatus.ReservStatsID))
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
            return View(reservationStatus);
        }

        // GET: ReservationStatuses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservationStatus = await _context.ReservationStatuses
                .FirstOrDefaultAsync(m => m.ReservStatsID == id);
            if (reservationStatus == null)
            {
                return NotFound();
            }

            return View(reservationStatus);
        }

        // POST: ReservationStatuses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservationStatus = await _context.ReservationStatuses.FindAsync(id);
            _context.ReservationStatuses.Remove(reservationStatus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationStatusExists(int id)
        {
            return _context.ReservationStatuses.Any(e => e.ReservStatsID == id);
        }
    }
}
