using _9dt.Web.Data;
using _9dt.Web.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _9dt.Web.Repositories
{
    public class MoveRepository : IMoveRepository
    {
        private readonly Context _context;
        public MoveRepository(Context context)
        {
            _context = context;
        }

        public int AddMove(Guid gameId, MoveInfo move)
        {
            if (!_context.GameMoves.ContainsKey(gameId))
                _context.GameMoves.Add(gameId, new List<MoveInfo>());

            _context.GameMoves[gameId].Add(move);

            return _context.GameMoves[gameId].Count-1;  // return 0 based move number.
        }

        public IList<MoveInfo> GetGameMoves(Guid gameId)
        {
            if (_context.GameMoves.ContainsKey(gameId))
                return _context.GameMoves[gameId];

            return null;    // return null or empty list?
        }

        public IList<MoveInfo> GetGameMoves(Guid gameId, int startIndex, int endIndex)
        {
            var moves = _context.GameMoves[gameId];
            return moves.Skip(startIndex).Take(endIndex - startIndex + 1).ToList();
        }

        public MoveInfo GetGameMove(Guid gameId, int move_number)
        {
            var moves = _context.GameMoves[gameId];
            return moves[move_number];
        }
    }
}
