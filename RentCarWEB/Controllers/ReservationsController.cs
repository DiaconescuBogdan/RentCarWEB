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
    public class ReservationsController : Controller
    {
        private readonly CarRentalContext _context;

        public ReservationsController(CarRentalContext context)
        {
            _context = context;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            var carRentalContext = _context.Reservations.Include(r => r.Car).Include(r => r.Coupon).Include(r => r.Customer).Include(r => r.ReservationStatus);
            return View(await carRentalContext.ToListAsync());
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Car)
                .Include(r => r.Coupon)
                .Include(r => r.Customer)
                .Include(r => r.ReservationStatus)
                .FirstOrDefaultAsync(m => m.ReservationId == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        public IActionResult Create()
        {
            ViewData["CarId"] = new SelectList(_context.Cars, "CarId", "CarId");
            ViewData["CouponId"] = new SelectList(_context.Coupons, "CouponCode", "CouponCode");
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId");
            ViewData["ReservationStatusId"] = new SelectList(_context.ReservationStatuses, "ReservStatsID", "ReservStatsID");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string plate, int customerid, DateTime startdate, DateTime enddate, string location, string couponcode, Reservation reservation)
        {
            if (_context.Cars.Where(c => c.Plate == plate && c.Location == location).FirstOrDefault() != null)
            {
                Car MyCar = _context.Cars.Where(c => c.Plate == plate && c.Location == location).FirstOrDefault();
                reservation.CarId = MyCar.CarId;
                reservation.Plate = MyCar.Plate;
                reservation.Location = MyCar.Location;
                if (_context.Customers.Where(c => c.CustomerId == customerid).FirstOrDefault() != null)
                {
                    Customer MyCustomer = _context.Customers.Where(c => c.CustomerId == customerid).FirstOrDefault();
                    reservation.CustomerId = MyCustomer.CustomerId;
                    reservation.StartDate = startdate;
                    reservation.EndDate = enddate;
                    if ((reservation.StartDate <= reservation.EndDate) && (reservation.StartDate >= DateTime.Now))
                    {

                        if (_context.Reservations.Where(c => (c.EndDate < reservation.StartDate || c.StartDate > reservation.EndDate)).Any())
                        {
                            _context.Add(reservation);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));
                        }
                        else if (_context.Reservations.Where(c => c.Plate == reservation.Plate).Any() == false)
                        {
                            _context.Add(reservation);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
            return View();
        }
        //public async Task<IActionResult> Create([Bind("ReservationId,StartDate,EndDate,Location,Plate,CarId,CustomerId,CouponId,ReservationStatusId")] Reservation reservation)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(reservation);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CarId"] = new SelectList(_context.Cars, "CarId", "CarId", reservation.CarId);
        //    ViewData["CouponId"] = new SelectList(_context.Coupons, "CouponCode", "CouponCode", reservation.CouponId);
        //    ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", reservation.CustomerId);
        //    ViewData["ReservationStatusId"] = new SelectList(_context.ReservationStatuses, "ReservStatsID", "ReservStatsID", reservation.ReservationStatusId);
        //    return View(reservation);
        //}

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "CarId", "CarId", reservation.CarId);
            ViewData["CouponId"] = new SelectList(_context.Coupons, "CouponCode", "CouponCode", reservation.CouponId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", reservation.CustomerId);
            ViewData["ReservationStatusId"] = new SelectList(_context.ReservationStatuses, "ReservStatsID", "ReservStatsID", reservation.ReservationStatusId);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string plate, int customerid, DateTime startdate, DateTime enddate, string location, string couponcode, Reservation reservation)
        {
            if (id != reservation.ReservationId)
            {
                return NotFound();
            }

            if (_context.Cars.Where(c => c.Plate == plate && c.Location == location).FirstOrDefault() != null)
            {
                Car MyCar = _context.Cars.Where(c => c.Plate == plate && c.Location == location).FirstOrDefault();
                reservation.CarId = MyCar.CarId;
                reservation.Plate = MyCar.Plate;
                reservation.Location = MyCar.Location;
                if (_context.Customers.Where(c => c.CustomerId == customerid).FirstOrDefault() != null)
                {
                    Customer MyCustomer = _context.Customers.Where(c => c.CustomerId == customerid).FirstOrDefault();
                    reservation.CustomerId = MyCustomer.CustomerId;
                    reservation.StartDate = startdate;
                    reservation.EndDate = enddate;
                    if ((reservation.StartDate <= reservation.EndDate) && (reservation.StartDate >= DateTime.Now))
                    {

                        if (_context.Reservations.Where(c => (c.EndDate < reservation.StartDate || c.StartDate > reservation.EndDate)).Any())
                        {
                            _context.Update(reservation);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));
                        }
                        else if (_context.Reservations.Where(c => c.Plate == reservation.Plate).Any() == false)
                        {
                            _context.Update(reservation);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
            return View();
        }

        // GET: Reservations/Delete/5
        [Authorize(Roles = "admin, manager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Car)
                .Include(r => r.Coupon)
                .Include(r => r.Customer)
                .Include(r => r.ReservationStatus)
                .FirstOrDefaultAsync(m => m.ReservationId == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, manager")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.ReservationId == id);
        }
    }
}
