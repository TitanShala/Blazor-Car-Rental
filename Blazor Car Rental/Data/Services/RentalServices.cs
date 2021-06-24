using Blazor_Car_Rental.Data.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazor_Car_Rental.Areas.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;

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

        public string CreateRental(Rental rental, int carId, string userName)
        {
            var user = _context.Users.Where(u => u.UserName == userName).FirstOrDefault();
            string userId = user.Id;
            rental.UserId = userId;
            rental.CarId = carId;
            Car car = _context.Cars.Where(c => c.Id == carId).FirstOrDefault();
            car.rentalcount += 1;
            _context.Cars.Update(car);
            _context.Rentals.Add(rental);
            _context.SaveChanges();
            return "Rental Created Succesfully";
        }
        public async Task<List<Rental>> GetRentals ()
        {
            List<Rental> Rentals = _context.Rentals.Include(r=> r.Car).ToList();
            return Rentals;
        }

        public async Task<List<Rental>> GetMyRentals(string userName)
        {
            var user = await _context.Users.Where(u => u.UserName == userName).FirstOrDefaultAsync();
            List<Rental> Rentals = await _context.Rentals.Include(r => r.Car).Where(r=> r.UserId.Equals(user.Id)).ToListAsync();
            return Rentals;
        }

        public string Update(Rental rental)
        {
            _context.Rentals.Update(rental);
            _context.SaveChanges();
            return "Update Succesfully";
        }

        public string DeleteRental(int id)
        {
            Rental rental = _context.Rentals.Where(c => c.Id == id).FirstOrDefault();
            _context.Rentals.Remove(rental);
            _context.SaveChanges();
            return "Rental Deleted Succesfully";
        }

        public IdentityUser getRentalUser(string id)
        {
            IdentityUser user =  _context.Users.Where(u => u.Id.Equals(id)).FirstOrDefault();
            return user;
        }
        public void SetToReturned(Rental rental)
        {
            rental.Returned = true;
            _context.SaveChanges();
        }
        public double getReview(int carId)
        {
            List<Rental> rentals = _context.Rentals.Where(r => r.CarId == carId && (r.Rate != 0 && r.Rate != null) ).ToList();
            double a = 0;
            foreach(var rent in rentals)
            {
                a += (double)rent.Rate;
            }
            return a/rentals.Count;
        }

    
        public async Task<Rental> getRental(string rentalId)
        {
            int id = Int32.Parse(rentalId);
            Rental rental = _context.Rentals.Where(r => r.Id == id).Include(r=> r.Car).FirstOrDefault();
            return rental;
        }

        public bool ValidOrderDate(DateTime RecDate, DateTime RetDate,DateTime? today, int carId)
        {
            Car car = _context.Cars.Where(c => c.Id == carId).FirstOrDefault();
            //check if time is valid
            //DateTime? today = DateTime.Today;
            if (RecDate < today || RecDate > RetDate)
            {
                return false;
            }

            //Check if client choosed a bussy slot
            //List<Rental> rentals = car.Rentals.ToList();
            List<Rental> rentals = _context.Rentals.Where(r => r.CarId == carId && r.Returned == false).ToList();
            rentals = rentals.OrderBy(r => r.ReceiptDate).ToList();
            foreach (var rent in rentals)
            {
                if ((RecDate >= rent.ReceiptDate && RecDate <= rent.ReturnDate) || (RetDate >= rent.ReceiptDate && RetDate <= rent.ReturnDate))               
                {
                    return false;
                }
            }
            return true;
        }

        public List<Car> getFilteredCars(DateTime RecDate, DateTime RetDate, List<Car> allCars)
        {
            List<Car> cars = new List<Car>();
            foreach(var c in allCars)
            {
                List<Rental> rentals = _context.Rentals.Where(r => r.Car == c).ToList();
                List<Rental> returnedRentals = rentals.Where(r => r.Returned == true).ToList();

                // 1) check if car doesn't have rentals
                // 2) check if all car rentals are returned 
                // 3) check if car is free for the requested dates
                if (rentals.Count < 1)
                {
                    cars.Add(c);
                }
                else if(returnedRentals.Count == rentals.Count)
                {
                    cars.Add(c);
                }
                else
                {
                    rentals = rentals.Where(r => r.Returned == false).OrderBy(r => r.ReceiptDate).ToList();
                    foreach (var rent in rentals)
                    {
                        //Check if the date is not the same
                        if (rent.ReceiptDate != RecDate && rent.ReturnDate != RetDate)
                        {
                            //If the requested receiptDate is between the rent date Or the requested return date is between the return date!
                            if ( (RecDate < rent.ReceiptDate && RetDate > rent.ReturnDate) || (RecDate >= rent.ReceiptDate && RecDate <= rent.ReturnDate) || (RetDate >= rent.ReceiptDate && RetDate <= rent.ReturnDate))
                            {
                                //Check if the car exists in the available car list, if it does, remove it
                                if (cars.Contains(rent.Car))
                                {
                                    cars.Remove(rent.Car);
                                }

                            }
                            //If the requested receiptDate is not between the rent date Or the requested return date is not between the return date!
                            else
                            {
                                //If available cars does not contain the car, add the car to the list
                                if (!cars.Contains(rent.Car))
                                {
                                    cars.Add(rent.Car);
                                }
                            }
                            
                        }
                    }
                }

            }

            return cars ;
        }
    }
}
