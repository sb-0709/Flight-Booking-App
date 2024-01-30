using Microsoft.AspNetCore.Mvc;
using flightclientapp.Models;
using FlightProjApi.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
// using System.Net.Http.Json;

namespace flightclientapp{
    public class LoginController:Controller{
        private readonly ILogger<LoginController> _logger;
        private readonly ISession session;
        public LoginController(ILogger<LoginController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            session = httpContextAccessor.HttpContext.Session;
        }
        [HttpGet]
        public ActionResult SignUp(){
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SignUp(LoginSignUp l){
            // Emp Emplobj = new Emp();
            using (var httpClient = new HttpClient())
            {
                //encoding the input from client
                StringContent content = new StringContent(JsonConvert.SerializeObject(l), Encoding.UTF8, "application/json");

                //now passing the data to controller in api which will take this data and add in db
                //here we are just doing verification if data is added or not
                using (var response = await httpClient.PostAsync("http://localhost:5089/api/Login", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var loginobj = JsonConvert.DeserializeObject<LoginSignUp>(apiResponse);
                }
            }
            return RedirectToAction("Login");
        }
        [HttpGet]
        public ActionResult Login(){
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginSignUp l)
        {
            ViewBag.message=" ";
            if(l.Email=="admin@gmail.com" && l.Pword=="admin"){
                HttpContext.Session.SetString("uname", "Admin");
                return RedirectToAction("ShowAllFlights", "Admin");
            }
            
            StringContent content = new StringContent(JsonConvert.SerializeObject(l), Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync("http://localhost:5089/api/Login/LoginAuthentication", content);
                if(response.StatusCode == System.Net.HttpStatusCode.NotFound){
                    ViewBag.message = "\nIncorrect Login or Account does not exist!";
                        return View();
                }
                else{
                    // System.Console.WriteLine("response"+response);
                    HttpContext.Session.SetString("uname", l.Email);
                    return RedirectToAction("HomeSearch", "Home");
                }
            }
        }

        public async Task<ActionResult> Logout(){
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

    }
}