using _9dt.Web.Data;
using _9dt.Web.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _9dt.Web.Repositories
{
    public class GameInfoRepository: IGameInfoRepository
    {
        private readonly Context _context;
        public GameInfoRepository(Context context)
        {
            _context = context;
        }
        
        public void Add(Guid gameId, GameInfo info)
        {
            _context.GameInfos.Add(gameId, info);
        }

        public IList<Guid> GetActiveGames()
        {
            return _context.GameInfos
                .Where(x => x.Value.Status == GameStatus.IN_PROGRESS)
                .Select(x => x.Key).ToList();
        }

        public GameInfo GetGameInfo(Guid gameId)
        {
            if (_context.GameInfos.ContainsKey(gameId))
                return _context.GameInfos[gameId];

            return null;
        }   
    }
}
