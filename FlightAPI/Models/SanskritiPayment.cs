using System;
using System.Collections.Generic;

namespace FlightProjApi.Models;

public partial class SanskritiPayment
{
    public int? Payid { get; set; }

    public string? CardHolderName { get; set; }

    public string Accno { get; set; } = null!;

    public DateOnly? ValidUpto { get; set; }

    public int? Cvv { get; set; }

    public virtual SanskritiPassenger? Pay { get; set; }
}
