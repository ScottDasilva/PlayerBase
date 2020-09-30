using System;
using System.ComponentModel.DataAnnotations;

namespace PlayerBase_3.Models
{
    public class Event
    {
        public Event()
        {
        }

        [Key]
        public int Id { get; set; }
        public int TeamId { get; set; }
        public string Title { get; set; }
        [Required,Display(Name = "Start Time")]
        public DateTime? StartTime { get; set; }
        [Display(Name = "End Time")]
        public DateTime? EndTime { get; set; }
        public string Location { get; set; }
        [Required]
        public DateTime? Date { get; set; }
        public string Description { get; set; }
    }
}
