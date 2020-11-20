using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayerBase_3.Models
{
    public class Player
    {
        public Player()
        {
        }

        [Key]
        public int Id { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        [ForeignKey("Team")]
        public int? TeamId { get; set; }
        [Required, Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required, Display(Name = "Last Name")]
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }
        public string Position { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        [Display(Name = "Games Played")]
        public int GamesPlayed { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }
        [Display(Name = "Penalty Minutes")]
        public int PenaltyMinutes {get; set;}

        public bool HasTeam()
        {
            return TeamId != null;
        }
        
    }
}
