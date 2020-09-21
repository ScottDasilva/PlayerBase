using System;
using System.ComponentModel.DataAnnotations;

namespace PlayerBase_2.Models
{
    public class Coach
    {
        public Coach()
        {
        }

        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }

        public string Province { get; set; }

        public int TeamId { get; set; }

        public string Email { get; set; }

        public string VideoUrl { get; set; }

        public int Wins { get; set; }

        public int Losses { get; set; }

        public int OTL { get; set; }

        public int PositionFinished { get; set; }

        public string Notes { get; set; }
    }
}
