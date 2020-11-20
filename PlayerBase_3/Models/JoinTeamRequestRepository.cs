using PlayerBase_3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerBase_3.Models
{
    public class JoinTeamRequestRepository : IJoinTeamRequestRepository
    {
        private readonly ApplicationDbContext context;
        public JoinTeamRequestRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public JoinTeamRequest Add(JoinTeamRequest joinTeamRequest)
        {
            context.JoinTeamRequest.Add(joinTeamRequest);
            context.SaveChanges();
            return joinTeamRequest;
        }

        public JoinTeamRequest GetJoinTeamRequestByPlayerId(int playerId)
        {
            return context.JoinTeamRequest.FirstOrDefault(JoinTeamRequest => JoinTeamRequest.PlayerId == playerId);
        }

        public JoinTeamRequest Delete(int Id)
        {
            JoinTeamRequest joinTeamRequest = context.JoinTeamRequest.Find(Id);
            if (joinTeamRequest != null)
            {
                context.JoinTeamRequest.Remove(joinTeamRequest);
                context.SaveChanges();
            }
            return joinTeamRequest;
        }

        public List<JoinTeamRequest> GetJoinTeamRequests(int teamId)
        {
            var joinTeamRequests = context.JoinTeamRequest.Where(jt => jt.TeamId == teamId).ToList();

            return joinTeamRequests;
        }
    }
}
