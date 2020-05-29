using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentCarWEB.Models
{
    public class ReservationStatus
    {
        [Key]
        public int ReservStatsID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}