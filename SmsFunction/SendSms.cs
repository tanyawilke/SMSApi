using SmsFunction.Enums;
using SmsFunction.Models;
using System;
using System.Threading.Tasks;

namespace SmsFunction
{
    public class SendSms
    {
        public SendSms()
        {

        }
        public async Task<int> SendSmsMessage(string recipient, string originator, string messageData)
        {
            var messageStatus = 0;

            try
            {
                ASPSMS.SMS tASPSMS = new ASPSMS.SMS();
                tASPSMS.AddRecipient(recipient);
                tASPSMS.Originator = originator;
                tASPSMS.MessageData = messageData;

                await tASPSMS.SendTextSMS();

                if (tASPSMS.ErrorCode == 1)
                {
                    messageStatus = (int)MessageStatus.Success;
                }
                else
                {
                    messageStatus = (int)MessageStatus.Failed;
                    //ViewBag.Status = "Error: " + tASPSMS.ErrorCode + " " + tASPSMS.ErrorCodeDescription;
                }
            }
            catch (Exception ex)
            {
                // ViewBag.Status = "Error: " + ex.Message;
            }

            return messageStatus;
        }
    }
}
