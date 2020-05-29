using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RentCarWEB.Models;

namespace RentCarWEB.Controllers
{
    
    public class CarsController : Controller
    {
        private readonly CarRentalContext _context;

        public CarsController(CarRentalContext context)
        {
            _context = context;
        }

        // GET: Cars
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cars.ToListAsync());
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .FirstOrDefaultAsync(m => m.CarId == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        [Authorize(Roles = "admin, manager")]
        // GET: Cars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin, manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarId,Plate,Manufacturer,Model,PricePerDay,Location")] Car car)
        {
            if (ModelState.IsValid)
            {
                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        [Authorize(Roles = "admin, manager")]
        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        [Authorize(Roles = "admin, manager")]
        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarId,Plate,Manufacturer,Model,PricePerDay,Location")] Car car)
        {
            if (id != car.CarId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.CarId))
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
            return View(car);
        }

        [Authorize(Roles = "admin, manager")]
        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .FirstOrDefaultAsync(m => m.CarId == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }
        [Authorize(Roles = "admin, manager")]
        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.CarId == id);
        }

        //public ActionResult AvailableCars([Bind("StartDate,EndDate,Location")] Reservation reservation)
        //{
        //    List<Car> carList = null;
        //    if (ModelState.IsValid)
        //    {
        //        if (reservation.StartDate != null && reservation.EndDate != null && reservation.Location != null)
        //        {
        //            carList = _context.Database.SqlQuery<Car>("Select * from Cars WHERE Location = @location AND CarID NOT IN" +
        //                 "(Select CarID FROM Reservations WHERE NOT (StartDate > @endDate) OR (EndDate < @startDate))",
        //                 new SqlParameter("location", reservation.Location), new SqlParameter("endDate", reservation.EndDate), new SqlParameter("startDate", reservation.StartDate)).ToList<Car>();
        //        }
        //        else if (reservation.StartDate == null && reservation.EndDate == null && reservation.Location != null)
        //        {
        //            carList = _context.Database.SqlQuery<Car>("Select * from Cars WHERE Location = @location",
        //                 new SqlParameter("location", reservation.Location)).ToList<Car>();
        //        }
        //        else if (reservation.StartDate != null && reservation.EndDate != null && reservation.Location == null)
        //        {
        //            carList = _context.Database.SqlQuery<Car>("Select * from Cars WHERE CarID NOT IN" +
        //                "(Select CarID FROM Reservations WHERE NOT (StartDate > @endDate) OR (EndDate < @startDate))",
        //                new SqlParameter("endDate", reservation.EndDate), new SqlParameter("startDate", reservation.StartDate)).ToList<Car>();
        //        }
        //    }
        //    if (carList == null)
        //    {
        //        ModelState.AddModelError("", "No available cars");
        //    }
        //    return View(carList);
        //}
    }
}
