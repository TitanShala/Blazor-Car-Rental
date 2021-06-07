using Blazor_Car_Rental.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_Car_Rental.Areas.Identity.Models
{
    public class UserIdentity : IdentityUser
    {
        ICollection<Rental> Rentals { get; set; }
    }
}
