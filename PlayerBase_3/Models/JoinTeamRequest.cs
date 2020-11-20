using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerBase_3.Models
{
    public class JoinTeamRequest
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Player")]
        public int PlayerId { get; set; }
        [ForeignKey("Team")]
        public int TeamId { get; set; }
    }
}
