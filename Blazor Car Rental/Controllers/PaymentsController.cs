using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazor_Car_Rental.Areas.Administrator.Services;
using Blazor_Car_Rental.Data;
using Blazor_Car_Rental.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using Stripe;
using Stripe.Checkout;


namespace Blazor_Car_Rental.Controllers
{
    [Route("create-payment-intent")]
    [ApiController]
    public class PaymentIntentApiController : Controller
    {
        private AdmCarServices admCarService;
        public PaymentIntentApiController(AdmCarServices _admCarService)
        {
            StripeConfiguration.ApiKey = "sk_test_51Ixf90DWzkrn83Gap2f7UH0PGnb7Tt8GFGOtKh9g7DC8MpcUVzX0hQXCZ51haZzmptyvzCzrqHQlAWXhpnSeWCBN00xjKgGRHk";
            admCarService = _admCarService;
        }
        [HttpPost]
        public ActionResult Create(PaymentIntentCreateRequest request)
        {
            var paymentIntents = new PaymentIntentService();
            var paymentIntent = paymentIntents.Create(new PaymentIntentCreateOptions
            {
                Amount = CalculateOrderAmount(request.Items),
                Currency = "eur",
            });
            return Json(new { clientSecret = paymentIntent.ClientSecret });
        }
        private int CalculateOrderAmount(Item[] items)
        {
            int CarId;
            Car car = new Car();
            foreach(var item in items)
            {
                CarId = Int32.Parse(item.Id);
                car = admCarService.GetCar(CarId).Result;
            }
            int price = Convert.ToInt32(car.Price) * 100;
            price = (int)(price - (price * 0.1)); //10% DISCOUNT
            return price;
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
}

