using Blazor_Car_Rental.Areas.Identity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_Car_Rental.Data.Models
{
    public class Rental
    {
        [Key]
        public int Id { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [Required]
        //[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ReceiptDate { get; set; }

        [Required]
        //[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ReturnDate { get; set; }

        public bool Returned { get; set; }
    }
}
