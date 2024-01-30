using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace flightclientapp.Models;

public partial class Flights
{
    [Display(Name ="Flight ID")]
    public int Flightid { get; set; }

    [Display(Name ="Airlines Name")]
    [Required(ErrorMessage ="Flight Name is Mandatory")]
    public string? Fname { get; set; }

    [Display(Name ="Source")]
    [Required(ErrorMessage ="Flight Source is Mandatory")]
    public string? Fsource { get; set; }
    [Display(Name ="Destination")]
    [Required(ErrorMessage ="Flight Destination is Mandatory")]
    public string? Fdestination { get; set; }

    [Display(Name ="Arrival Date and Time")]
    [Required(ErrorMessage ="Arrival Date and Time is Mandatory")]
    [DataType(DataType.DateTime)]
    public DateTime ArrivalTime { get; set; }

    [Display(Name ="Departure Date and Time")]
    [Required(ErrorMessage ="Departure Date and Time is Mandatory")]
    [DataType(DataType.DateTime)]
    public DateTime DepartureTime { get; set; }

    [DataType(DataType.Currency)]
    [Required(ErrorMessage ="Rate is Mandatory")]
    public decimal? Rate { get; set; }

    [Display(Name ="Number of Seats Available")]
    [Required(ErrorMessage ="Number of Seats is Mandatory")]
    public int? NoOfSeats { get; set; }

    [Display(Name ="Arrival Airport ID")]
    [Required(ErrorMessage ="Arrival Airport ID is Mandatory")]
    public string? ArrAirportId { get; set; }

    [Display(Name ="Departure Airport ID")]
    [Required(ErrorMessage ="Departure Airport ID is Mandatory")]
    public string? DeptAirportId { get; set; }

    [Display(Name ="Arrival Terminal")]
    [Required(ErrorMessage ="Arrival Terminal is Mandatory")]
    public int? FarrTerminal { get; set; }

    [Display(Name ="Departure Terminal")]
    [Required(ErrorMessage ="Departure Terminal is Mandatory")]
    public int? FdeptTerminal { get; set; }
}