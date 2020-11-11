using PlayerBase_3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerBase_3.Models
{
    public class TeamRepository : ITeamRepository
    {
        private readonly ApplicationDbContext context;
        public TeamRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Team Add(Team team)
        {
            context.Teams.Add(team);
            context.SaveChanges();
            return team;
        }

        public Team Delete(int Id)
        {
            Team team = context.Teams.Find(Id);
            if (team != null)
            {
                context.Teams.Remove(team);
                context.SaveChanges();
            }

            return team;
        }

        public IEnumerable<Team> GetAllTeams()
        {
            return context.Teams;
        }

        public Team GetTeam(int Id)
        {
            return context.Teams.Find(Id);
        }

        public Team Update(Team teamChanges)
        {
            var team = context.Teams.Attach(teamChanges);
            team.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

            return teamChanges;
        }

        public List<Player> GetPlayers(int id)
        {
            var players = context.Players.ToList();

            players = players.Where(pt => pt.TeamId == id).ToList();

            return players;
        }
    }
}
