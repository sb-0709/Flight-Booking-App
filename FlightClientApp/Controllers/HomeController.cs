using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FlightClient.Models;
using flightclientapp.Models;
using FlightProjApi.Models;
using Newtonsoft.Json;
using System.Text;
// using AspNetCore;

namespace FlightClient.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ISession session;
    public static List<Flights> allFlights;

    public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        session = httpContextAccessor.HttpContext.Session;
    }

    [HttpGet]
    public ActionResult HomeSearch(){
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> HomeSearch(HomeSearch h){
        ViewBag.mess="";
        if(h.Fsource!=h.Fdestination){
            HttpContext.Session.SetString("source", h.Fsource);
            HttpContext.Session.SetString("destination", h.Fdestination);
            HttpContext.Session.SetString("date", h.Date.ToString());
        
        string year = DateTime.Parse(h.Date.ToString()).Year.ToString();
        string month = DateTime.Parse(h.Date.ToString()).Month.ToString(); 
        string day = DateTime.Parse(h.Date.ToString()).Day.ToString();
        string dow = DateTime.Parse(h.Date.ToString()).DayOfWeek.ToString(); 
        
        // StringContent content = new StringContent(JsonConvert.SerializeObject(h), Encoding.UTF8, "application/json");
        using (var httpClient = new HttpClient())
        {
            List<Flights> flights = new List<Flights>();
            var response = await httpClient.GetAsync("http://localhost:5089/api/Home?source="+h.Fsource+"&destination="+h.Fdestination);
            System.Console.WriteLine("response: "+response);
            if(response.StatusCode == System.Net.HttpStatusCode.NotFound){
                ViewBag.message = "\nNo Flights Found!";
                // System.Console.WriteLine("response" + response);
                return View();
            }
            else{
                var AccResponse = response.Content.ReadAsStringAsync().Result;
                List<Flights> f = JsonConvert.DeserializeObject<List<Flights>>(AccResponse);
                f = f.ToList();
                // System.Console.WriteLine("flights: "+f);
                foreach (var item in f)
                {
                    System.Console.WriteLine(item.Fname);
                    if(DateOnly.FromDateTime((DateTime)item.DepartureTime) == h.Date && h.NoOfPass<=item.NoOfSeats){
                        // System.Console.WriteLine("in loop: "+item);
                        flights.Add(item);
                    }
                }
                allFlights = flights;

                return RedirectToAction("ShowFlights");
            }          
        }
        }
        else{
            ViewBag.mess = "Source and Destination cannot be same!";
            return View();
        }
    }

    public IActionResult ShowFlights(){
        // foreach (var item in allFlights)
        // {
        //     System.Console.WriteLine("flights "+item.Fsource + " " + item.DepartureTime);
        // }
        ViewBag.source = HttpContext.Session.GetString("source");
        ViewBag.destination = HttpContext.Session.GetString("destination");
        ViewBag.date = HttpContext.Session.GetString("date");
        return View(allFlights);
    }

    public IActionResult BookNow(int id){
        HttpContext.Session.SetString("flid", id.ToString());
        ViewBag.Username = HttpContext.Session.GetString("uname");
        if(ViewBag.Username!=null){
            return RedirectToAction("PassengerDetails");
        }
        else{
            return RedirectToAction("Login", "Login");
        } 
    }

    [HttpGet]
        public ActionResult PassengerDetails(){
            return View();
        }
    [HttpPost]
    // public async Task<ActionResult> PassengerDetails(Passenger p){
    //     if(ModelState.IsValid){
    //         HttpContext.Session.SetString("fname", p.Pfname);
    //         HttpContext.Session.SetString("lname", p.Plname);
    //         HttpContext.Session.SetString("email", p.Pemail);
    //         HttpContext.Session.SetString("phno", p.PhoneNo);
    //         HttpContext.Session.SetString("address", p.Paddress);
    //         HttpContext.Session.SetString("passno", p.SeatNo);
    //         HttpContext.Session.SetString("food", p.Food);

    //         // using (var httpClient = new HttpClient())
    //         // {
    //         //     //encoding the input from client
    //         //     StringContent content = new StringContent(JsonConvert.SerializeObject(p), Encoding.UTF8, "application/json");

    //         //     using (var response = await httpClient.PostAsync("http://localhost:5089/api/Booking", content))
    //         //     {
    //         //         string apiResponse = await response.Content.ReadAsStringAsync();
    //         //         // var passobj = JsonConvert.DeserializeObject<Passenger>(apiResponse);
    //         //     }
    //         // }

    //         return RedirectToAction("Booking");
    //     }
    //     else{
    //         return View();
    //     }
    // }
    public async Task<ActionResult> PassengerDetails(Passenger p){
        //add passenger details
        using (var httpClient = new HttpClient())
        {
            //encoding the input from client
            StringContent content = new StringContent(JsonConvert.SerializeObject(p), Encoding.UTF8, "application/json");

            using (var response = await httpClient.PostAsync("http://localhost:5089/api/Booking", content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                // var passobj = JsonConvert.DeserializeObject<Passenger>(apiResponse);
            }
            
            var response1 = await httpClient.GetAsync("http://localhost:5089/api/Booking/GetId?fname="+p.Pfname+"&lname="+p.Plname+"&phno="+p.PhoneNo);
            // System.Console.WriteLine("response: "+response);
            if(response1.StatusCode == System.Net.HttpStatusCode.NotFound){
                return View();
            }
            else{
                var AccResponse = response1.Content.ReadAsStringAsync().Result;
                Passenger pres = JsonConvert.DeserializeObject<Passenger>(AccResponse);
                
                Bookings b = new Bookings();

                //adding booking details and assigning values
                b.FlightId = Convert.ToInt32(HttpContext.Session.GetString("flid"));
                b.CustId = pres.Pid;
                System.Console.WriteLine("pid + "+pres.Pid);
                b.BookingDate = DateTime.Now.Date;
                System.Console.WriteLine("date "+b.BookingDate);
                b.NoOfPass = Convert.ToInt32(p.SeatNo);
                System.Console.WriteLine("b.noofpass "+b.NoOfPass);

                //fetching flight details to get the rate 
                // StringContent content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");
                var response2 = await httpClient.GetAsync("http://localhost:5089/api/Admin/" + b.FlightId);
                if(response2.StatusCode == System.Net.HttpStatusCode.NotFound){
                    return View();
                }
                else
                {
                    var apiResponse = response2.Content.ReadAsStringAsync().Result;
                    Flights flight = JsonConvert.DeserializeObject<Flights>(apiResponse);

                    //calculating total cost
                    decimal cost = (decimal)flight.Rate;
                    System.Console.WriteLine("cost+"+cost);
                    
                    string food = p.Food;
                    if(food == "Veg"){
                        b.TotalCost  = (cost + 100 )* b.NoOfPass;
                    }
                    else if(food == "NV"){
                        b.TotalCost  = (cost + 200 )* b.NoOfPass;
                    }
                    else{
                        b.TotalCost = cost * b.NoOfPass;
                    }

                //storing values in bookings table
                //encoding the input from client
                StringContent content2 = new StringContent(JsonConvert.SerializeObject(b), Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await httpClient.PostAsync("http://localhost:5089/api/Booking/Booking", content2))
                {
                    string bookResponse = await response.Content.ReadAsStringAsync();
                    if(response.IsSuccessStatusCode){
                        return RedirectToAction("Successful");
                    }
                    // var passobj = JsonConvert.DeserializeObject<Passenger>(apiResponse);
                }
                return View();
                }
            }
        }
    }

    //.............
    public async Task<ActionResult> Booking(){

        Bookings b = new Bookings();

        //adding booking details and assigning values
        b.FlightId = Convert.ToInt32(HttpContext.Session.GetString("flid"));
        // b.CustId = 
        b.BookingDate = DateTime.Now.Date;
        System.Console.WriteLine("date "+b.BookingDate);
        b.NoOfPass = Convert.ToInt32(HttpContext.Session.GetString("passno"));
        System.Console.WriteLine("b.noofpass "+b.NoOfPass);
        Flights flight = new Flights();

        //fetching flight details to get the rate 
        using (var httpClient = new HttpClient())
        {
            // StringContent content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");
            using (var response = await httpClient.GetAsync("http://localhost:5089/api/Admin/" + b.FlightId))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                ViewBag.Result = "Success";
                flight = JsonConvert.DeserializeObject<Flights>(apiResponse);
            }
        }

        //calculating total cost
        decimal cost = (decimal)flight.Rate;
        string food = HttpContext.Session.GetString("food");
        if(food == "Veg"){
            b.TotalCost  = (cost + 100 )* b.NoOfPass;
        }
        else if(food == "NV"){
            b.TotalCost  = (cost + 200 )* b.NoOfPass;
        }
        else{
            b.TotalCost = cost * b.NoOfPass;
        }

        //storing values in bookings table
        using (var httpClient = new HttpClient())
        {
            //encoding the input from client
            StringContent content = new StringContent(JsonConvert.SerializeObject(b), Encoding.UTF8, "application/json");

            using (var response = await httpClient.PostAsync("http://localhost:5089/api/Booking/Booking", content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                // var passobj = JsonConvert.DeserializeObject<Passenger>(apiResponse);
            }
        }

        return RedirectToAction("Successful");
    }

    public ActionResult Successful(){
        return View();
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
