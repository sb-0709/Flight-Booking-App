namespace FlightProjApi.Repository{
    public interface ILoginRepo<SanskritiLoginDetail>{
        SanskritiLoginDetail Login(SanskritiLoginDetail l);
        void SignUp(SanskritiLoginDetail l);

        List<SanskritiLoginDetail> GetAllDetails();
        SanskritiLoginDetail GetAcc(int id);
    }
}