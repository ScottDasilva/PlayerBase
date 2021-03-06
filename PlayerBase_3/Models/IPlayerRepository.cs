﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerBase_3.Models
{
    public interface IPlayerRepository
    {
        Player GetPlayer(int Id);
        Player GetPlayerByUserId(string userId);
        IEnumerable<Player> GetAllPlayers();
        Player Add(Player player);
        Player Update(Player playerChanges);
        Player Delete(int Id);
    }
}
