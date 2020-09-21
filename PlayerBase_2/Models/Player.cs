using System;
namespace PlayerBase_2.Models
{
    public class Player
    {
        public Player()
        {
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? BirthDate { get; set; }

        public string Position { get; set; }

        public string Province { get; set; }

        public string City { get; set; }

        public int TeamId { get; set; }

        public string Email { get; set; }

        public int GamesPlayed { get; set; }

        public int Goals { get; set; }

        public int Assists { get; set; }

        public int PenaltyMinutes {get; set;}
    }
}
