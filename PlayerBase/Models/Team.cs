using System;
using System.Collections.Generic;

namespace PlayerBase.Models
{
    public class Team
    {
        public Team()
        {
        }

        public int Id { get; set; }

        public string City { get; set; }

        public string Province { get; set; }

        public string Name { get; set; }

        public string League { get; set; }

        public string Abbreviation { get; set; }

        public byte[] Logo { get; set; }

        public string Email { get; set; }

        public List<Player> Players { get; set; }

    }
}
