using Blazor_Car_Rental.Data.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_Car_Rental.Data.Services
{
    public class RentalServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnivronment;

        public RentalServices(ApplicationDbContext context, IWebHostEnvironment hostEnivronment)
        {
            _context = context;
            _hostEnivronment = hostEnivronment;
        }

        public async Task<string> CreateCarAsync(Rental rental, int carId, string userName)
        {
            var user = _context.Users.Where(u => u.UserName == userName).FirstOrDefault();
            string userId = user.Id;
            rental.UserId = userId;
            rental.CarId = carId;
            _context.Rentals.Add(rental);
            _context.SaveChanges();
            return "Rental Created Succesfully";
        }
        public async Task<List<Rental>> GetRentals ()
        {
            List<Rental> Rentals = _context.Rentals.ToList();
            return Rentals;
        }

        /*public Car GetRental(int id)
        {
            Car car = _context.Cars.FirstOrDefault(c => c.Id == id);
            return car;
        }

        public string UpdateCar(Car car)
        {
            _context.Cars.Update(car);
            _context.SaveChanges();
            return "Update Succesfully";
        }*/

        public string DeleteRental(int id)
        {
            Rental rental = _context.Rentals.Where(c => c.Id == id).FirstOrDefault();
            _context.Rentals.Remove(rental);
            _context.SaveChanges();
            return "Rental Deleted Succesfully";
        }
    }
}
