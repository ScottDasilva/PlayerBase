using System;
using System.ComponentModel.DataAnnotations;

namespace PlayerBase_2.Models
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

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public string Arena { get; set; }

        public DateTime? date { get; set; }

        public string Description { get; set; }
    }
}
