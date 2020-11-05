using PlayerBase_3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerBase_3.Models
{
    public class GameRepository : IGameRepository
    {
        private readonly ApplicationDbContext context;

        public GameRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public Game Add(Game game)
        {
            context.Games.Add(game);
            context.SaveChanges();
            return game;
        }

        public Game Delete(int Id)
        {
            Game game = context.Games.Find(Id);
            if (game != null)
            {
                context.Games.Remove(game);
                context.SaveChanges();
            }

            return game;
        }

            public IEnumerable<Game> GetAllGames()
        {
            return context.Games;
        }

        public Game GetGame(int Id)
        {
            return context.Games.Find(Id);
        }

        public Game Update(Game gameChanges)
        {
            var game = context.Games.Attach(gameChanges);
            game.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

            return gameChanges;
        }
    }
}
