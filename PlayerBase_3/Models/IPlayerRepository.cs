using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerBase_3.Models
{
    interface IPlayerRepository
    {
        Player GetPlayer(int Id);
        IEnumerable<Player> GetAllPlayers();
        Player Add(Player player);
        Player Update(Player playerChanges);
        Player Delete(int Id);
    }
}
