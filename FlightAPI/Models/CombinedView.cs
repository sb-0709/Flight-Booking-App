using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FlightProjApi.Models;

public partial class CombinedView{
    public int BookingId { get; set; }

    public string CustName { get; set; }
    public string Airline { get; set; }
    public string Source { get; set; }
    public string Destination { get; set; }
    public DateOnly BookingDate { get; set; }

    public int NoOfPass { get; set; }
    public decimal Rate { get; set; }
}