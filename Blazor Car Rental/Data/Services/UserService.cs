using Blazor_Car_Rental.Areas.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazor_Car_Rental.Areas.Identity.Models;
using Microsoft.AspNetCore.Identity;

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

        public List<IdentityUser> getUsers()
        {
            List<IdentityUser> users = _context.Users.ToList();
            List<IdentityUserRole<string>> userRoles = _context.UserRoles.ToList();
            List<IdentityUser> list = new List<IdentityUser>();
            string UserRoleId = _context.Roles.Where(u => u.Name.Equals("User")).ToArray().First().Id;
            foreach (var user in users)
            {
                IdentityUserRole<string> role = userRoles.Where(u => u.UserId.Equals(user.Id)).ToList().First();
                if (role.RoleId.Equals(UserRoleId))
                {
                    list.Add(user);
                }
            }
            return list;

        }

        public List<IdentityUser> getAdmins()
        {
            List<IdentityUser> users = _context.Users.ToList();
            List<IdentityUserRole<string>> userRoles = _context.UserRoles.ToList();
            List<IdentityUser> list = new List<IdentityUser>();
            string UserRoleId = _context.Roles.Where(u => u.Name.Equals("Admin")).ToArray().First().Id;
            foreach (var user in users)
            {
                IdentityUserRole<string> role = userRoles.Where(u => u.UserId.Equals(user.Id)).ToList().First();
                if (role.RoleId.Equals(UserRoleId))
                {
                    list.Add(user);
                }
            }
            return list;
        }
    }
}
