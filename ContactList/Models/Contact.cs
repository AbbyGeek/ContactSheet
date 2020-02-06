using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace ContactList.Models
{
    public class Contact
    {
        public int id { get; set; }
        [Display(Name="First Name")]
        [Required]
        public string firstName { get; set; }
        [Display(Name = "Last Name")]
        [Required]
        public string lastName { get; set; }
        [Display(Name = "Company")]
        [Required]
        public string company { get; set; }
        [Display(Name = "Email Address")]
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage ="You Must Provide A Phone Number")]
        [RegularExpression(@"^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$", ErrorMessage = "Not a valid phone number")]
        public string primaryPhone { get; set; }
        [Display(Name = "Secondary Phone Number")]
        [RegularExpression(@"^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$", ErrorMessage = "Not a valid phone number")]
        public string secondaryPhone { get; set; }
        [Display(Name = "Address")]
        [Required]
        public string address { get; set; }

    }
}