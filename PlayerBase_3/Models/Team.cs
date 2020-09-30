using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlayerBase_3.Models
{
    public class Team
    {
        public Team()
        {
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Province { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string League { get; set; }
        public string Abbreviation { get; set; }
        public string Email { get; set; }
        public List<Player> Players { get; set; }

    }
}
