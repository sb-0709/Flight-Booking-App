using System.Data.Common;
using FlightProjApi.Models;

namespace FlightProjApi.Repository{
    public class LoginRepo : ILoginRepo<SanskritiLoginDetail>
    {
        private readonly Ace52024Context db;
        public LoginRepo(){}
        public LoginRepo(Ace52024Context _db){
            db = _db;
        }
        public SanskritiLoginDetail Login(SanskritiLoginDetail l)
        {
            var result = (from i in db.SanskritiLoginDetails
                        where i.Email==l.Email && i.Pword == l.Pword
                        select i).SingleOrDefault();
            return result;
        }

        public void SignUp(SanskritiLoginDetail l)
        {
            db.SanskritiLoginDetails.Add(l);
            db.SaveChanges();
        }

        public List<SanskritiLoginDetail> GetAllDetails(){
            return db.SanskritiLoginDetails.ToList();
        }

        public SanskritiLoginDetail GetAcc(int id){
            return db.SanskritiLoginDetails.Find(id);
        }
    }
}