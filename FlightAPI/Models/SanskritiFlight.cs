using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlightProjApi.Models;

public partial class SanskritiFlight
{
    [Display(Name ="Flight ID")]
    public int Flightid { get; set; }

    [Display(Name ="Airlines Name")]
    public string? Fname { get; set; }

    [Display(Name ="Source")]
    public string? Fsource { get; set; }
    [Display(Name ="Destination")]
    public string? Fdestination { get; set; }

    [Display(Name ="Arrival Date and Time")]
    [DataType(DataType.DateTime)]
    public DateTime ArrivalTime { get; set; }

    [Display(Name ="Departure Date and Time")]
    [DataType(DataType.DateTime)]
    public DateTime DepartureTime { get; set; }

    [DataType(DataType.Currency)]
    public decimal? Rate { get; set; }

    [Display(Name ="Number of Seats Available")]
    public int? NoOfSeats { get; set; }

    [Display(Name ="Arrival Airport ID")]
    public string? ArrAirportId { get; set; }

    [Display(Name ="Departure Airport ID")]
    public string? DeptAirportId { get; set; }

    [Display(Name ="Arrival Terminal")]
    public int? FarrTerminal { get; set; }

    [Display(Name ="Departure Terminal")]
    public int? FdeptTerminal { get; set; }
    public virtual ICollection<SanskritiBooking> SanskritiBookings { get; set; } = new List<SanskritiBooking>();
}
