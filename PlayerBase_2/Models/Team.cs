using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlayerBase_2.Models
{
    public class Team
    {
        public Team()
        {
        }

        [Key]
        public int Id { get; set; }

        public string City { get; set; }

        public string Province { get; set; }

        public string Name { get; set; }

        public string League { get; set; }

        public string Abbreviation { get; set; }

        public string LogoUrl { get; set; }

        public string Email { get; set; }


        public List<Player> Players { get; set; }

    }
}
