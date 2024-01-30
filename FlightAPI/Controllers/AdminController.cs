using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlightProjApi.Models;
using FlightProjApi.Services;

namespace FlightProjApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IFlightServ<SanskritiFlight> flserv;

        public AdminController(IFlightServ<SanskritiFlight> _flserv)
        {
            flserv=_flserv;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SanskritiFlight>>> GetSanskritiFlights()
        {
            return flserv.GetAllFlights(); 
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SanskritiFlight>> GetSanskritiFlight(int id)
        {
            var flight = flserv.GetFlightById(id);

            if (flight == null)
            {
                return NotFound();
            }

            return flight;
        }

        // PUT: api/Employee/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSanskritiFlight(int id, SanskritiFlight flight)
        {
            if (id != flight.Flightid)
            {
                return BadRequest();
            }

           // _context.Entry(employee).State = EntityState.Modified;
        try
        {
          flserv.UpdateFlight(id, flight);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SanskritiFlightExists(id))
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

        // POST: api/Employee
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SanskritiFlight>> PostSanskritiFlight(SanskritiFlight flight)
        {
            
            try
            {
            flserv.AddFlight(flight);
            }
            catch (DbUpdateException)
            {
                if (SanskritiFlightExists(flight.Flightid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetSanskritiFlight", new { id = flight.Flightid }, flight);
            // return CreatedAtAction("GetEmployee", new { id = employee.Eid }, employee);
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            SanskritiFlight flight = flserv.GetFlightById(id);
            if (flight == null)
            {
                return NotFound();
            }

            flserv.DeleteFlight(id);
            return NoContent();
        }

        private bool SanskritiFlightExists(int id)
        {
            SanskritiFlight f= flserv.GetFlightById(id);
            if(f!=null)
            return true;
            else
            return false;
        }
        
        
        
        // private readonly Ace52024Context _context;

        // public AdminController(Ace52024Context context)
        // {
        //     _context = context;
        // }

        // // GET: api/Admin
        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<SanskritiFlight>>> GetSanskritiFlights()
        // {
        //     return await _context.SanskritiFlights.ToListAsync();
        // }

        // // GET: api/Admin/5
        // [HttpGet("{id}")]
        // public async Task<ActionResult<SanskritiFlight>> GetSanskritiFlight(int id)
        // {
        //     var sanskritiFlight = await _context.SanskritiFlights.FindAsync(id);

        //     if (sanskritiFlight == null)
        //     {
        //         return NotFound();
        //     }

        //     return sanskritiFlight;
        // }

        // // PUT: api/Admin/5
        // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutSanskritiFlight(int id, SanskritiFlight sanskritiFlight)
        // {
        //     if (id != sanskritiFlight.Flightid)
        //     {
        //         return BadRequest();
        //     }

        //     _context.Entry(sanskritiFlight).State = EntityState.Modified;

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!SanskritiFlightExists(id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }

        //     return NoContent();
        // }

        // // POST: api/Admin
        // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPost]
        // public async Task<ActionResult<SanskritiFlight>> PostSanskritiFlight(SanskritiFlight sanskritiFlight)
        // {
        //     _context.SanskritiFlights.Add(sanskritiFlight);
        //     await _context.SaveChangesAsync();

        //     return CreatedAtAction("GetSanskritiFlight", new { id = sanskritiFlight.Flightid }, sanskritiFlight);
        // }

        // // DELETE: api/Admin/5
        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteSanskritiFlight(int id)
        // {
        //     var sanskritiFlight = await _context.SanskritiFlights.FindAsync(id);
        //     if (sanskritiFlight == null)
        //     {
        //         return NotFound();
        //     }

        //     _context.SanskritiFlights.Remove(sanskritiFlight);
        //     await _context.SaveChangesAsync();

        //     return NoContent();
        // }

        // private bool SanskritiFlightExists(int id)
        // {
        //     return _context.SanskritiFlights.Any(e => e.Flightid == id);
        // }
    }
}
