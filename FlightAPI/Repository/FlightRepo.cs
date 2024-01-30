using System.Data.Common;
using FlightProjApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightProjApi.Repository{
    public class FlightRepo : IFlightRepo<SanskritiFlight>
    {
        private readonly Ace52024Context db;
        public FlightRepo(){}

        public FlightRepo(Ace52024Context _db)
        {
            db=_db;
        }
        public void AddFlight(SanskritiFlight f)
        {
            try{
                db.SanskritiFlights.Add(f);
                db.SaveChanges();
            }
            catch(Exception e){
                Console.WriteLine(e.Message);
            }
        }

        public void DeleteFlight(int id)
        {
            try{
                SanskritiFlight f = db.SanskritiFlights.Find(id);
                db.SanskritiFlights.Remove(f);
                db.SaveChanges();
            }
            catch(Exception e){
                Console.WriteLine(e.Message);
            }
        }

        public List<SanskritiFlight> GetAllFlights()
        {
            List<SanskritiFlight> flights = db.SanskritiFlights.ToList();
            return flights;
        }

        public SanskritiFlight GetFlightById(int id)
        {
            SanskritiFlight f = db.SanskritiFlights.Find(id);
            return f;
        }

        public void UpdateFlight(int id, SanskritiFlight f)
        {
            db.SanskritiFlights.Update(f);
            db.SaveChanges();
        }
    }
}