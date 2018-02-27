using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmsApi.Models
{
    public class CountryModel
    {
        public int id { get; set; }
        public string Name { get; set; }
        public int country_code { get; set; }
        public int mobile_country_code { get; set; }
        public decimal sms_price { get; set; }
    }
}
