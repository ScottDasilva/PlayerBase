using System;
namespace PlayerBase.Models
{
    public class Game
    {
        public Game()
        {
        }

        public int id { get; set; }

        public int HomeTeamId { get; set; }

        public int AwayTeamId { get; set; }

        public int HomeGoals { get; set; }

        public int AwayGoal { get; set; }

        public string League { get; set; }

        public string Season { get; set; }
    }
}
