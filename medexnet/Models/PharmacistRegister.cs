using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace medexnet.Models
{
    public class PharmacistRegister
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "You must have a first name.")]
        public string fName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "You must have a last name.")]
        public string lName { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "You must have an email address.")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Display(Name = "Confirm Email Address")]
        [DataType(DataType.EmailAddress)]
        [Compare("email", ErrorMessage = "The Emails do not match.")]
        public string confirmEmail { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "You must have a password.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Password must be above 10 characters.")]
        public string password { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "The passwords do not match.")]
        public string confirmPassword { get; set; }

        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "You must have a phone number.")]
        public string phoneNumber { get; set; }
    }
}