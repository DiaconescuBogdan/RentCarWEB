using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace RentCarWEB.Models
{
    public class Car
    {  
        [Key]
        public int CarId { get; set; }
        public string Plate { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public float PricePerDay { get; set; }
        public string Location { get; set; }

        public  ICollection<Reservation> Reservations { get; set; }
    }
}