using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentCarWEB.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Location { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}