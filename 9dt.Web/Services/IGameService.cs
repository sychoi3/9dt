using _9dt.Web.Data;
using _9dt.Web.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _9dt.Web.Services
{
    public interface IGameService
    {
        IList<Guid> GetInProgressGames();
        Guid CreateGame(CreateGameDto request);
        GameStateDto GetGameState(Guid gameId);
        IList<MoveInfoDto> GetMoves(Guid gameId, int? start, int? end);
        int MakeMove(Guid gameId, string playerId, int column);
        MoveInfoDto GetMove(Guid gameId, int move_number);
        void QuitGame(Guid gameId, string playerId);
    }
}
