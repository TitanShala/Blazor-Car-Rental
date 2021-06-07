using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_Car_Rental.Data.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        public int RentalId { get; set; }

        [ForeignKey("RentalId")]
        public Rental Rental { get; set; }

        public double Price { get; set; }

        public DateTime Date { get; set; }
    }
}
