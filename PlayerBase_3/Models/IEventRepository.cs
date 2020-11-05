using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerBase_3.Models
{
    interface IEventRepository
    {
        Event GetEvent(int Id);
        IEnumerable<Event> GetAllEvents();
        Event Add(Event teamEvent);
        Event Update(Event eventChanges);
        Event Delete(int Id);
    }
}
