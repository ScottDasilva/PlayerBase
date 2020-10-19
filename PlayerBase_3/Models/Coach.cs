using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayerBase_3.Models
{
    public class Coach
    {
        public Coach()
        {
        }

        [Key]
        public int Id { get; set; }
        [ForeignKey("Team")]
        public int TeamId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Email { get; set; }
        public string VideoUrl { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int OTL { get; set; }
        public int PositionFinished { get; set; }
        public string Notes { get; set; }
    }
}
