using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentCarWEB.Models
{
    public class CarRentalContext : IdentityDbContext<IdentityUser>
    {
        public CarRentalContext(DbContextOptions<CarRentalContext> options)
            : base(options)
        { }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<ReservationStatus> ReservationStatuses { get; set; }
    }
}
