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
    public class BookingController : ControllerBase
    {
        private readonly Ace52024Context db;
        public BookingController(Ace52024Context _db)
        {
            db=_db;
        }
        [HttpGet]
        [Route("PassengerDetails")]
        public async Task<ActionResult<IEnumerable<SanskritiPassenger>>> GetPassengers(){
            return db.SanskritiPassengers.ToList();
        }

        [HttpPost]
        public async Task<ActionResult<SanskritiPassenger>> AddPassenger(SanskritiPassenger p){
            db.SanskritiPassengers.Add(p);
            db.SaveChanges();
            return CreatedAtAction("GetPassenegers", new { id = p.Pid }, p);
        }

        [HttpGet]
        [Route("GetId")]
        public async Task<ActionResult<SanskritiPassenger>> GetPassengerId(string fname, string lname, string phno){
            var pass = (from i in db.SanskritiPassengers
                         where i.Pfname == fname && i.Plname == lname && i.PhoneNo == phno
                         select i).FirstOrDefault();
            if(pass!=null){
            return pass;
            }
            else{
                return NotFound();
            }
        }
        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<CombinedView>>> GetBookings(){
        //     var result = db.SanskritiBookings.Include(p => p.Flight).Include(c=>c.Cust).Select(m=>new CombinedView{
        //         BookingId  = m.BookingId,
        //         CustName = m.Cust.Pfname,
        //         Airline = m.Flight.Fname,
        //         Source = m.Flight.Fsource,
        //         Destination = m.Flight.Fdestination,
        //         BookingDate = (DateOnly)m.BookingDate,
        //         NoOfPass = (int)m.NoOfPass,
        //         Rate = (decimal)m.TotalCost,
        //     }).ToList();
        //     // var flights = db.SanskritiBookings.Include(x=>x.Flight);
        //     // var pass = db.SanskritiBookings.Include(x=>x.Cust)
        //     return result;
        // }


         [HttpGet("{id}")]
        public async Task<ActionResult<SanskritiBooking>> GetBookingById(int id)
        {
            var booking = db.SanskritiBookings.Find(id);

            if (booking == null)
            {
                return NotFound();
            }

            return booking;
        }
        [HttpGet]
        [Route("BookingDetails")]
        public async Task<ActionResult<IEnumerable<SanskritiBooking>>> GetBookings(){
            return db.SanskritiBookings.ToList();
        }

        [HttpPost]
        [Route("Booking")]
        public async Task<ActionResult<SanskritiPassenger>> AddBooking(SanskritiBooking b){
            db.SanskritiBookings.Add(b);
            db.SaveChanges();
            return CreatedAtAction("GetBookings", new { id = b.BookingId }, b);
        }
    }
}