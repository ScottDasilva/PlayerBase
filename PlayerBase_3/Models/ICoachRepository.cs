using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerBase_3.Models
{
    interface ICoachRepository
    {
        Coach GetCoach(int Id);
        IEnumerable<Coach> GetAllCoaches();
        Coach Add(Coach coach);
        Coach Update(Coach coachChanges);
        Coach Delete(int Id);
    }
}
