using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmsApi.Models;

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
    }
}