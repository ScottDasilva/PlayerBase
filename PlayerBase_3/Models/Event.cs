using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayerBase_3.Models
{
    public class Event
    {
        public Event()
        {
        }

        [Key]
        public int Id { get; set; }
        [ForeignKey("Team")]
        public int TeamId { get; set; }
        public string Title { get; set; }
        [Required,Display(Name = "Start Time")]
        [DataType(DataType.Time)]
        public DateTime? StartTime { get; set; }
        [Display(Name = "End Time")]
        [DataType(DataType.Time)]
        public DateTime? EndTime { get; set; }
        public string Location { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }
        public string Description { get; set; }
    }
}
