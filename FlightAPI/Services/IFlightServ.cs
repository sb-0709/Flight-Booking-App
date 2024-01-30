namespace FlightProjApi.Services{
    public interface IFlightServ<SanskritiFlight>{
        List<SanskritiFlight> GetAllFlights();
        void AddFlight(SanskritiFlight f);
        void UpdateFlight(int id, SanskritiFlight f);
        SanskritiFlight GetFlightById(int id);
        void DeleteFlight(int id);
    }
}