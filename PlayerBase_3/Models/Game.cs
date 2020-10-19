using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayerBase_3.Models
{
    public class Game
    {
        public Game()
        {
        }

        [Key]
        public int Id { get; set; }
        [ForeignKey("Team")]
        public int HomeTeamId { get; set; }
        [ForeignKey("Team")]
        public int AwayTeamId { get; set; }

        public int HomeGoals { get; set; }

        public int AwayGoal { get; set; }

        public string League { get; set; }

        public string Season { get; set; }
    }
}
