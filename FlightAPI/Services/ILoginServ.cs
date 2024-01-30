namespace FlightProjApi.Services{
    public interface ILoginServ<SanskritiLoginDetail>{
        void SignUp(SanskritiLoginDetail l);

        SanskritiLoginDetail Login(SanskritiLoginDetail l);
        List<SanskritiLoginDetail> GetAllDetails();
        SanskritiLoginDetail GetAcc(int id);
    }
}