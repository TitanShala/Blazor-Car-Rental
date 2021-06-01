using BlazorInputFile;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_Car_Rental.Data.Models
{
    public class Car
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Make length should between 2 and 30")]
        public string Make { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Model length should between 2 and 30")]
        public string Model { get; set; }

        [Required]
        [Range(1980, 2021)]
        public int Year { get; set; }

        [Required]
        [Range(3, 5)]
        public int Doors { get; set; }

        [Required]
        [Range(0, 15)]
        public int Luggages { get; set; }

        [Required]
        [Range(40, 9999)]
        [Display(Name = "Power HP")]
        public int Power { get; set; }

        [Required]
        [StringLength(30)]
        public string Transmission { get; set; }

        [Required]
        [StringLength(20)]
        public string Fuel { get; set; }

        [Required]
        [Range(0.8, 10)]
        public double EngineDisplacement { get; set; }

        [Required]
        [Range(1, 9999)]
        [DataType(DataType.Currency)]
        public double Price { get; set; }


        [MaxLength(500)]
        [Display(Name = "Car Image")]

        public String ImageName { get; set; }

        [NotMapped]
        [Display(Name = "Upload Image")]
        public IFileListEntry ImageFile { get; set; }

        [Required]
        public bool Rented { get; set; }

        [Required]
        public int rentalcount { get; set; }
        public ICollection<Rental> Rentals { get; set; }

    }
}
