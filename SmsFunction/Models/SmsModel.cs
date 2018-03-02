using System;
using System.Collections.Generic;
using System.Text;

namespace SmsFunction.Models
{
    public class SmsModel
    {
        public string Originator { get; set; }

        public string Recipient { get; set; }

        public string MessageData { get; set; }
    }
}
