using System;
using System.ComponentModel.DataAnnotations;

namespace PlayerBase_2.Models
{
    public class User
    {
        public User()
        {
        }

        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public string UserRole { get; set; }

        public string ProfilePictureUrl { get; set; }
    }
}
