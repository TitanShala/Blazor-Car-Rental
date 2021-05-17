using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_Car_Rental.Data.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 5, ErrorMessage = "Make length should between 5 and 60")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Make length should between 2 and 50")]
        public string Subject { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 5, ErrorMessage = "Make length should between 5 and 500")]
        public string Text { get; set; }
    }
}
