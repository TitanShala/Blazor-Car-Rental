using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlinePayment.Models
{
    public class Item
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
