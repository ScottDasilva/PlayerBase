using PlayerBase_3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerBase_3.Models
{
    public class CoachRepository : ICoachRepository
    {
        private readonly ApplicationDbContext context;

        public CoachRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public Coach Add(Coach coach)
        {
            context.Coaches.Add(coach);
            context.SaveChanges();
            return coach;
        }

        public Coach Delete(int Id)
        {
            Coach coach = context.Coaches.Find(Id);
            if (coach != null)
            {
                context.Coaches.Remove(coach);
                context.SaveChanges();
            }

            return coach;
        }

        public IEnumerable<Coach> GetAllCoaches()
        {
            return context.Coaches;
        }

        public Coach GetCoach(int Id)
        {
            return context.Coaches.Find(Id);
        }

        public Coach Update(Coach coachChanges)
        {
            var coach = context.Coaches.Attach(coachChanges);
            coach.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

            return coachChanges;
        }
    }
}
