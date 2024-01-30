using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace flightclientapp.Models;

public partial class Bookings
{
    [Display(Name ="Booking ID")]
    public int BookingId { get; set; }

    public int? FlightId { get; set; }

    public int? CustId { get; set; }

    [Display(Name ="Date of Booking")]
    public DateTime? BookingDate { get; set; }

    [Display(Name ="Number of Passengers")]
    public int? NoOfPass { get; set; }

    [Display(Name ="Total Amount Paid")]
    public decimal? TotalCost { get; set; }
}