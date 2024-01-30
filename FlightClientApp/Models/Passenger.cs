using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace FlightProjApi.Models;

[Serializable]
public partial class Passenger
{
    public int Pid { get; set; }

    [Display(Name ="First Name")]
    [Required(ErrorMessage ="Passenger First Name is Mandatory")]
    public string Pfname { get; set; }

    [Display(Name ="Last Name")]
    [Required(ErrorMessage ="Passenger Last Name is Mandatory")]
    public string Plname { get; set; }

    [Display(Name ="Email Address")]
    [Required(ErrorMessage ="Email Address is Mandatory")]
    [DataType(DataType.EmailAddress,ErrorMessage ="Please enter a valid email address")]
    public string Pemail { get; set; }

    [Display(Name ="Phone Number")]
    [Required(ErrorMessage ="Phone Number is Mandatory")]
    [StringLength(maximumLength:10,MinimumLength =10,ErrorMessage ="Phone no should be exactly 10 digits")]
    public string PhoneNo { get; set; }

    [Display(Name ="Permenant Address")]
    [Required(ErrorMessage ="Permenant Address is Mandatory")]
    public string Paddress { get; set; }

    [Display(Name ="Number of Passengers")]
    public string SeatNo { get; set; }

    [Display(Name ="Do you want Food Service?")]
    public string Food { get; set; }
}