using FlightProjApi.Models;
using FlightProjApi.Repository;

namespace FlightProjApi.Services{
    public class LoginServ : ILoginServ<SanskritiLoginDetail>
    {
        private readonly ILoginRepo<SanskritiLoginDetail> repo;
        public LoginServ(){}
        public LoginServ(ILoginRepo<SanskritiLoginDetail> _repo){
            repo = _repo;
        }
        public SanskritiLoginDetail Login(SanskritiLoginDetail l)
        {
            return repo.Login(l);
        }

        public void SignUp(SanskritiLoginDetail l)
        {
            repo.SignUp(l);
        }
        public List<SanskritiLoginDetail> GetAllDetails(){
            return repo.GetAllDetails();
        }
        public SanskritiLoginDetail GetAcc(int id){
            return repo.GetAcc(id);
        }
    }
}