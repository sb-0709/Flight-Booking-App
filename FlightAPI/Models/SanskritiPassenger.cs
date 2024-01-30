using System;
using System.Collections.Generic;

namespace FlightProjApi.Models;

public partial class SanskritiPassenger
{
    public int Pid { get; set; }

    public string? Pfname { get; set; }

    public string? Plname { get; set; }

    public string? Pemail { get; set; }

    public string? PhoneNo { get; set; }

    public string? Paddress { get; set; }

    public string? SeatNo { get; set; }

    public string? Food { get; set; }

    public virtual ICollection<SanskritiBooking> SanskritiBookings { get; set; } = new List<SanskritiBooking>();

    public virtual ICollection<SanskritiPayment> SanskritiPayments { get; set; } = new List<SanskritiPayment>();
}
