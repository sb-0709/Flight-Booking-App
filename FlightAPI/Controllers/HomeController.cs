using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlightProjApi.Models;
using FlightProjApi.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FlightProjApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly Ace52024Context db;
        public HomeController(Ace52024Context _db)
        {
            db=_db;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SanskritiFlight>>> SearchFlights(string source, string destination){
            List<SanskritiFlight> flights = (from i in db.SanskritiFlights
                                             where i.Fsource==source && i.Fdestination==destination
                                             select i).ToList();
            if(flights!=null){
                return flights;
            }
            else{
                return NotFound();
            }
        }
    }
}