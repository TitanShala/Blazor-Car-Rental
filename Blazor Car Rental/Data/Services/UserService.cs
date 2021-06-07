using Blazor_Car_Rental.Areas.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_Car_Rental.Data.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<string> getUser(string id)
        {
            return _context.Users.Where(u => u.Id.Equals(id)).FirstOrDefault().UserName;
        }
    }
}
