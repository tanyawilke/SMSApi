using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmsApi.Models;
using SmsApi.Enums;

namespace SmsApi.Controllers
{
    [Produces("application/json")]
    [Route("api/SmsModels")]
    public class SmsModelsController : Controller
    {
        private readonly ApiContext _context;

        public SmsModelsController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/SmsModels
        [HttpGet]
        public IEnumerable<SmsModel> GetSms()
        {
            return _context.Sms;
        }

        // GET: api/SmsModels/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSmsModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var smsModel = await _context.Sms.SingleOrDefaultAsync(m => m.country_id == id);

            if (smsModel == null)
            {
                return NotFound();
            }

            return Ok(smsModel);
        }

        // PUT: api/SmsModels/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSmsModel([FromRoute] int id, [FromBody] SmsModel smsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != smsModel.country_id)
            {
                return BadRequest();
            }

            _context.Entry(smsModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SmsModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/SmsModels
        [HttpPost]
        public async Task<IActionResult> PostSmsModel([FromBody] SmsModel smsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sendMessage = SendSmsMessage(smsModel);

            _context.Sms.Add(smsModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSmsModel", new { id = smsModel.country_id }, smsModel);
        }

        // DELETE: api/SmsModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSmsModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var smsModel = await _context.Sms.SingleOrDefaultAsync(m => m.country_id == id);
            if (smsModel == null)
            {
                return NotFound();
            }

            _context.Sms.Remove(smsModel);
            await _context.SaveChangesAsync();

            return Ok(smsModel);
        }

        private bool SmsModelExists(int id)
        {
            return _context.Sms.Any(e => e.country_id == id);
        }

        public async Task<int> SendSmsMessage(SmsModel smsModel)
        {
            var messageStatus = 0;

            try
            {
                ASPSMS.SMS tASPSMS = new ASPSMS.SMS();
                tASPSMS.AddRecipient(smsModel.sender);
                tASPSMS.Originator = smsModel.message;
                tASPSMS.MessageData = smsModel.number.ToString();

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