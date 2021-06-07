using Blazor_Car_Rental.Areas.Administrator.Services;
using Blazor_Car_Rental.Data.Models;
using Blazor_Car_Rental.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_Car_Rental.Controllers
{
    [Route("success-payment")]
    [ApiController]
    public class SuccesfullPaymentController : Controller
    {
        private AdmCarServices admCarService;
        private RentalServices rentalService;
        private PaymentService paymentService;

        public SuccesfullPaymentController(AdmCarServices _admCarService, RentalServices _rentalService, PaymentService _paymentService)
        {
            admCarService = _admCarService;
            rentalService = _rentalService;
            paymentService = _paymentService;
        }

        [HttpPost]
        public void Create(PaymentIntentCreateRequest request)
        {
            Pay(request);
            
        }
        public void Pay(PaymentIntentCreateRequest request)
        {
            Item item = request.Items[0];
            Payment payment = new Payment();
            payment.RentalId = Int32.Parse(item.Id);
            Rental rental = rentalService.getRental(item.Id).Result;
            payment.Price = rental.Car.Price - (rental.Car.Price * 0.1);
            DateTime date = new DateTime();
            payment.Date = date;
            rental.Paid = true;
            rentalService.Update(rental);
            paymentService.Create(payment);
        }
    }
    public class Item
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
    public class PaymentIntentCreateRequest
    {
        [JsonProperty("items")]
        public Item[] Items { get; set; }
    }
}
