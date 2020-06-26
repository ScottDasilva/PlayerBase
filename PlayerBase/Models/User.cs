using System;
namespace PlayerBase.Models
{
    public class User
    {
        public User()
        {
        }

        public int UserId { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string UserRole { get; set; }

        public string ProfilePictureUrl { get; set; }
    }
}
