using System;
using System.ComponentModel.DataAnnotations;

namespace RentCarWEB.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
        public string Plate { get; set; }

        public int CarId { get; set; }
        public Car Car { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string CouponId { get; set; }
        public Coupon Coupon { get; set; }
        public int ReservationStatusId { get; set; }
        public ReservationStatus ReservationStatus { get; set; }
    }
}