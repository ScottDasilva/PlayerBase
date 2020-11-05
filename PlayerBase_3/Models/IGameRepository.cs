using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerBase_3.Models
{
    interface IGameRepository
    {
        Game GetGame(int Id);
        IEnumerable<Game> GetAllGames();
        Game Add(Game game);
        Game Update(Game gameChanges);
        Game Delete(int Id);
    }
}
