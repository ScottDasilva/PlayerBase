using PlayerBase_3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerBase_3.Models
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext context;

        public EventRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Event Add(Event teamEvent)
        {
            context.Events.Add(teamEvent);
            context.SaveChanges();
            return teamEvent;
        }

        public Event Delete(int Id)
        {
            Event teamEvent = context.Events.Find(Id);
            if (teamEvent != null)
            {
                context.Events.Remove(teamEvent);
                context.SaveChanges();
            }

            return teamEvent;
        }

        public IEnumerable<Event> GetAllEvents()
        {
            return context.Events;
        }

        public IEnumerable<Event> GetTeamEvents(int teamId)
        {
            return context.Events.Where(e => e.TeamId == teamId);
        }

        public Event GetEvent(int Id)
        {
            return context.Events.Find(Id);
        }

            public Event Update(Event eventChanges)
        {
            var teamEvent = context.Events.Attach(eventChanges);
            teamEvent.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

            return eventChanges;
        }
    }
}
