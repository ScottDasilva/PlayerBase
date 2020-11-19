using PlayerBase_3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerBase_3.Models
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly ApplicationDbContext context;
        public PlayerRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Player Add(Player player)
        {
            context.Players.Add(player);
            context.SaveChanges();
            return player;
        }

        public Player Delete(int Id)
        {
            Player player = context.Players.Find(Id);
            if(player != null)
            {
                context.Players.Remove(player);
                context.SaveChanges();
            }

            return player;
        }

        public IEnumerable<Player> GetAllPlayers()
        {
            return context.Players;
        }

        public Player GetPlayer(int Id)
        {
            return context.Players.Find(Id);
        }

        public Player GetPlayerByUserId(string userId)
        {
            return context.Players.FirstOrDefault(p => p.UserId == userId);
        }

        public Player Update(Player playerChanges)
        {
            var player = context.Players.Attach(playerChanges);
            player.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

            return playerChanges;
        }
    }
}
