using _9dt.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _9dt.Web.Repositories.Interfaces
{
    public interface IMoveRepository
    {
        int AddMove(Guid gameId, MoveInfo move);
        IList<MoveInfo> GetGameMoves(Guid gameId);
        IList<MoveInfo> GetGameMoves(Guid gameId, int startIndex, int endIndex);
        MoveInfo GetGameMove(Guid gameId, int move_number);
    }
}
