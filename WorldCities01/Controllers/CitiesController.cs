using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorldCities01.Data;
using WorldCities01.Models;

namespace WorldCities01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly WorldCities01Context _context;

        public CitiesController(WorldCities01Context context)
        {
            _context = context;
        }

        // GET: api/Cities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetCity()
        {
            return await _context.City.ToListAsync();
        }

        // GET: api/Cities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetCity(int id)
        {
            var city = await _context.City.FindAsync(id);

            if (city == null)
            {
                return NotFound();
            }

            return city;
        }

        [HttpPost]
        public async Task<IActionResult> PostCity(string name, string country, int population)
        {
            var city = new City
            {
                Name = name,
                Country = country,
                Population = population
            };
            _context.City.Add(city);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCity", new { id = city.CityId }, city);
        }

        private bool CityExists(int id)
        {
            return _context.City.Any(e => e.CityId == id);
        }
    }
}
