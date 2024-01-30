using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FlightProjApi.Models;

public partial class HomeSearch{
    [Display(Name ="Source")]
    [Required(ErrorMessage ="Source is Mandatory")]
    public string? Fsource { get; set; }
    
    [Display(Name ="Destination")]
    [Required(ErrorMessage ="Destination is Mandatory")]
    public string? Fdestination { get; set; }

    [Display(Name ="Date")]
    [DataType(DataType.Date)]
    // [DateGreaterThanOrEqualToToday(ErrorMessage = "Please enter a valid date greater than or equal to today")]
    [Required(ErrorMessage ="Date is Mandatory")]
    public DateOnly? Date { get; set; }

    [Display(Name ="Number of Passengers")]
    [Required(ErrorMessage ="Number of passengers is Mandatory")]
    public int NoOfPass { get; set; }

}