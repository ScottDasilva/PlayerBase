using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerBase_3.Models
{
    interface ITeamRepository
    {
        Team GetTeam(int Id);
        IEnumerable<Team> GetAllTeams();
        Team Add(Team team);
        Team Update(Team teamChanges);
        Team Delete(int Id);
    }
}
