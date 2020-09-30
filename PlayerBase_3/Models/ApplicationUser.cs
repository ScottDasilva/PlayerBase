using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerBase_3.Models
{
    public class ApplicationUser: IdentityUser
    {
        [Required, Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required, Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        [Display(Name = "Birth Date")]
        public DateTime? BirthDate { get; set; }
    }
}
