using FlightProjApi.Models;
using FlightProjApi.Repository;

namespace FlightProjApi.Services{
    public class FlightServ : IFlightServ<SanskritiFlight>
    {
        private readonly IFlightRepo<SanskritiFlight> flrepo;
        public FlightServ(){}

        public FlightServ(IFlightRepo<SanskritiFlight> _flrepo){
            flrepo = _flrepo;
        }
        public void AddFlight(SanskritiFlight f)
        {
            flrepo.AddFlight(f);
        }

        public void DeleteFlight(int id)
        {
            flrepo.DeleteFlight(id);
        }

        public List<SanskritiFlight> GetAllFlights()
        {
            return flrepo.GetAllFlights();
        }

        public SanskritiFlight GetFlightById(int id)
        {
            return flrepo.GetFlightById(id);
        }

        public void UpdateFlight(int id, SanskritiFlight f)
        {
            flrepo.UpdateFlight(id, f);
        }
    }
}