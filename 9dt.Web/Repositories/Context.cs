using _9dt.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _9dt.Web.Repositories
{
    public class Context
    {
        public Dictionary<Guid, GameBoard> Games;
        public Dictionary<Guid, IList<MoveInfo>> GameMoves;
        public Dictionary<Guid, GameInfo> GameInfos;
        
        public Context()
        {
            Games = new Dictionary<Guid, GameBoard>();
            GameMoves = new Dictionary<Guid, IList<MoveInfo>>();
            GameInfos = new Dictionary<Guid, GameInfo>();
        }
    }
}
