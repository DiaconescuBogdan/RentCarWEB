using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentCarWEB.Models
{
    public class Coupon
    {
        [Key]
        public string CouponCode { get; set; }
        public string Description { get; set; }
        public decimal Discount { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}