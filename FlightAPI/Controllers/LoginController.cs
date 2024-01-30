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
    public class LoginController : ControllerBase
    {
        private readonly ILoginServ<SanskritiLoginDetail> serv;

        public LoginController(ILoginServ<SanskritiLoginDetail> _serv)
        {
            serv=_serv;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SanskritiLoginDetail>>> GetSanskritiLoginDetails()
        {
            return serv.GetAllDetails(); 
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SanskritiLoginDetail>> GetSanskritiLoginDetail(int id)
        {
            var acc = serv.GetAcc(id);

            if (acc == null)
            {
                return NotFound();
            }

            return acc;
        }
        [Route("LoginAuthentication")]
        [HttpPost]
        public async Task<ActionResult<SanskritiLoginDetail>> SanskritiLogin(SanskritiLoginDetail l){
            SanskritiLoginDetail result = serv.Login(l);
            if(result!=null){
                return result;
            }
            else{
                return NotFound();
            }
        }


        // POST: api/Employee
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SanskritiLoginDetail>> PostSanskritiLoginDetail(SanskritiLoginDetail l)
        {
            try
            {
            serv.SignUp(l);
            }
            catch (DbUpdateException)
            {
                if (SanskritiLoginDetailExists(l.Pid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetSanskritiLoginDetail", new { id = l.Pid }, l);
            // return CreatedAtAction("GetEmployee", new { id = employee.Eid }, employee);
        }

        private bool SanskritiLoginDetailExists(int id)
        {
            SanskritiLoginDetail l= serv.GetAcc(id);
            if(l!=null)
            return true;
            else
            return false;
        }
    }
}