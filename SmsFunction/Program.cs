using System;
using System.Collections.Generic;
using System.Text;

namespace SmsFunction
{
    class Program
    {
        public static async System.Threading.Tasks.Task MainAsync(string[] args)
        {
            SendSms sms = new SendSms();
            int x = await sms.SendSmsMessage("Tanya", "Happy", "Message");
        }
    }
}
