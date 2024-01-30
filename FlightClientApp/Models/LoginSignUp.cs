using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightProjApi.Models;

public partial class LoginSignUp
{
    [Display(Name ="First Name")]
    [Required(ErrorMessage ="First Name is Mandatory")]
    public string? Fname { get; set; }

    [Display(Name ="Last Name")]
    [Required(ErrorMessage ="Last Name is Mandatory")]
    public string? Lname { get; set; }

    [Required(ErrorMessage ="Please enter an email")]
    [DataType(DataType.EmailAddress,ErrorMessage ="Please enter a valid email address")]
    public string? Email { get; set; }

    [Display(Name ="Password")]
    [Required(ErrorMessage ="Password is required")]
    [StringLength(maximumLength:100,MinimumLength =4,ErrorMessage ="Password should be atleast 4 characters long")]
    public string? Pword { get; set; }

    public int Pid { get; set; }

    [NotMapped]
    [Display(Name ="Confirm Password")]
    [Compare("Pword",ErrorMessage ="Passwords do not match")]
    public string? ConfirmPword{get;set;}
}