﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Blazor_Car_Rental.Data;
using Blazor_Car_Rental.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Blazor_Car_Rental.Data.Services;


namespace Blazor_Car_Rental.Areas.Administrator.Services
{
    
    public class AdmCarServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnivronment;

        public AdmCarServices (ApplicationDbContext context, IWebHostEnvironment hostEnivronment)
        {
            _context = context;
            _hostEnivronment = hostEnivronment;
    }

        public async Task<string> CreateCarAsync(Car car)
        {       
            car.Rented = false;
            _context.Cars.Add(car);
            _context.SaveChanges();
            return "Car Created Succesfully";
        } 

        public async Task<List<Car>> GetCars()
        {
            List<Car> Cars = _context.Cars.ToList();
            return Cars;
        }

        public async Task<List<Car>> GetTopCars()
        {
            List<Car> Cars = _context.Cars.OrderByDescending(c=> c.Year).ToList();
            //List<Car> Cars = _context.Cars.ToList();
            List<Car> TopCars = new List<Car>();
            if(Cars.Count > 2)
            {
                for (int i = 0; i < 3; i++)
                {
                    TopCars.Add(Cars[i]);
                }
            }
            else
            {
                for (int i = 0; i < Cars.Count; i++)
                {
                    TopCars.Add(Cars[i]);
                }
            }
            return TopCars;
        }

        public Car GetCar(int id)
        {
            Car car = _context.Cars.FirstOrDefault(c => c.Id == id);
            return car;
        }

        public string UpdateCar(Car car)
        {
            _context.Cars.Update(car);
            _context.SaveChanges();
            return "Update Succesfully";
        }

        public string DeleteCar(int id)
        {
            Car car = _context.Cars.Where(c => c.Id == id).FirstOrDefault();
            _context.Cars.Remove(car);
            _context.SaveChanges();
            return "Car Deleted Succesfully";
        }
    }
}
