using _9dt.Web.Data;
using _9dt.Web.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace _9dt.Web.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly Context _context;
        public GameRepository(Context context)
        {
            _context = context;
        }

        // add 
        public void AddGame(Guid gameId, GameBoard gb)
        {
            _context.Games.Add(gameId, gb);
        }

        // update.
        public GameBoard GetGame(Guid gameId)
        {
            if (_context.Games.ContainsKey(gameId))
                return _context.Games[gameId];

            return null;
        }

    }
}
