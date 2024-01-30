using System;
using System.Collections.Generic;

namespace FlightProjApi.Models;

public partial class SanskritiBooking
{
    public int BookingId { get; set; }

    public int? FlightId { get; set; }

    public int? CustId { get; set; }

    public DateTime? BookingDate { get; set; }

    public int? NoOfPass { get; set; }

    public decimal? TotalCost { get; set; }

    public virtual SanskritiPassenger? Cust { get; set; }

    public virtual SanskritiFlight? Flight { get; set; }
}
