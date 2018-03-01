using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmsApi.Models
{
    public class SmsModel
    {
        public int id { get; set; }
        [Key]
        public int country_id { get; set; }
        public DateTime date_sent { get; set; }
        public int number { get; set; }
        public string message { get; set; }
        public string sender { get; set; }
        public CountryModel Country { get; set; }
    }
}
