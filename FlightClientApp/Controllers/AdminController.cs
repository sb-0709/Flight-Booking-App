using Microsoft.AspNetCore.Mvc;
using flightclientapp.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
// using System.Net.Http.Json;

namespace flightclientapp{
    public class AdminController:Controller{
        public static List<Flights> flights = new List<Flights>();

        private readonly ILogger<AdminController> _logger;
        private readonly ISession session;

        public AdminController(ILogger<AdminController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            session = httpContextAccessor.HttpContext.Session;
        }
        public async Task<ActionResult> ShowAllFlights()
        {
            HttpClient client= new HttpClient();
            // List<Emp> EmpInfo = new List<Emp>();
            client.DefaultRequestHeaders.Clear();

            //Define request data format  
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
            HttpResponseMessage Res = await client.GetAsync("http://localhost:5089/api/Admin");

            //Checking the response is successful or not which is sent using HttpClient  
            if (Res.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                var AccResponse = Res.Content.ReadAsStringAsync().Result;

                //Deserializing the response recieved from web api and storing into the Employee list  
                flights = JsonConvert.DeserializeObject<List<Flights>>(AccResponse);

            }
            //returning the employee list to view  
            return View(flights);
        }

        public ActionResult AddFlight(){
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddFlight(Flights f){
            // Emp Emplobj = new Emp();
            using (var httpClient = new HttpClient())
            {
                //encoding the input from client
                StringContent content = new StringContent(JsonConvert.SerializeObject(f), Encoding.UTF8, "application/json");

                //now passing the data to controller in api which will take this data and add in db
                //here we are just doing verification if data is added or not
                using (var response = await httpClient.PostAsync("http://localhost:5089/api/Admin", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var flightobj = JsonConvert.DeserializeObject<Flights>(apiResponse);
                }
            }
            return RedirectToAction("ShowAllFlights");
        }

        [HttpGet]
        public async Task<ActionResult> EditFlight(int id)
        {
            // Emp emp = new Emp();
            Flights flight = new Flights();
            TempData["id"] = id;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5089/api/Admin/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    flight = JsonConvert.DeserializeObject<Flights>(apiResponse);
                }
            }
            return View(flight);
        }

        [HttpPost]
        public async Task<ActionResult> EditFlight(Flights f)
        {
            // Emp receivedemp = new Emp();

            using (var httpClient = new HttpClient())
            {
                f.Flightid = Convert.ToInt32(TempData["id"]);
                System.Console.WriteLine("id" + f.Flightid);
                StringContent content1 = new StringContent(JsonConvert.SerializeObject(f), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("http://localhost:5089/api/Admin/" + f.Flightid, content1))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    var flight = JsonConvert.DeserializeObject<Flights>(apiResponse);
                }
            }
            return RedirectToAction("ShowAllFlights");
        }

        [HttpGet]
        public async Task<ActionResult> DeleteFlight(int id)
        {
            TempData["fid"] = id;
            Flights flight  = new Flights();
            // Emp e = new Emp();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5089/api/Admin/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    flight = JsonConvert.DeserializeObject<Flights>(apiResponse);
                }
            }
            return View(flight);
        }


        [HttpPost]
       // [ActionName("DeleteEmployee")]
        public async Task<ActionResult> DeleteFlight(Flights f)
        {
            int fid = Convert.ToInt32(TempData["fid"]);
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("http://localhost:5089/api/Admin/" + fid))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("ShowAllFlights");
        }

         public async Task<ActionResult> GetFlightById(int id)
        {
            Flights flight = new Flights();
            using (var httpClient = new HttpClient())
            {
                // StringContent content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");
                using (var response = await httpClient.GetAsync("http://localhost:5089/api/Admin/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    flight = JsonConvert.DeserializeObject<Flights>(apiResponse);
                }
            }
            return View(flight);
        }

        // public async Task<ActionResult> CombinedView(){
        //     HttpClient client= new HttpClient();
        //     HttpResponseMessage Res = await client.GetAsync("http://localhost:5089/api/Booking");
        //     List<CombinedView> result = new List<CombinedView>();
        //     //Checking the response is successful or not which is sent using HttpClient  
        //     if (Res.IsSuccessStatusCode)
        //     {
        //         //Storing the response details recieved from web api   
        //         var AccResponse = Res.Content.ReadAsStringAsync().Result;

        //         //Deserializing the response recieved from web api and storing into the Employee list  
        //         result = JsonConvert.DeserializeObject<List<CombinedView>>(AccResponse);

        //     }
        //     //returning the employee list to view  
        //     return View(result);
        // }
    }

}