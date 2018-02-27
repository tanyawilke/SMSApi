using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmsApi.Models;
using Serilog;

namespace SmsApi.Controllers
{
    [Produces("application/json")]
    [Route("api/CountryModels")]
    public class CountryModelsController : Controller
    {
        private readonly ApiContext _context;
        private readonly ILogger _logger;

        public CountryModelsController(ApiContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/CountryModels
        [HttpGet]
        public IEnumerable<CountryModel> GetCountry()
        {
            var country = _context.Country;

            if (country == null)
            {
                _logger.Error(NotFound().ToString());
            }

            _logger.Information("List of countries retrieved successfully.");

            return country;
        }

        // GET: api/CountryModels/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCountryModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var countryModel = await _context.Country.SingleOrDefaultAsync(m => m.id == id);

            if (countryModel == null)
            {
                return NotFound();
            }

            return Ok(countryModel);
        }

        // PUT: api/CountryModels/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountryModel([FromRoute] int id, [FromBody] CountryModel countryModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != countryModel.id)
            {
                return BadRequest();
            }

            _context.Entry(countryModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryModelExists(id))
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

        // POST: api/CountryModels
        [HttpPost]
        public async Task<IActionResult> PostCountryModel([FromBody] CountryModel countryModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Country.Add(countryModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCountryModel", new { id = countryModel.id }, countryModel);
        }

        // DELETE: api/CountryModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountryModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var countryModel = await _context.Country.SingleOrDefaultAsync(m => m.id == id);
            if (countryModel == null)
            {
                return NotFound();
            }

            _context.Country.Remove(countryModel);
            await _context.SaveChangesAsync();

            return Ok(countryModel);
        }

        private bool CountryModelExists(int id)
        {
            return _context.Country.Any(e => e.id == id);
        }
    }
}