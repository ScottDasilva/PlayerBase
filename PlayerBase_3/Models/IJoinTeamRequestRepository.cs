using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerBase_3.Models
{
    public interface IJoinTeamRequestRepository
    {
        JoinTeamRequest Add(JoinTeamRequest joinTeamRequest);
        JoinTeamRequest GetJoinTeamRequestByPlayerId(int playerId);
        JoinTeamRequest Delete(int Id);
        List<JoinTeamRequest> GetJoinTeamRequests(int teamId);
    }
}
