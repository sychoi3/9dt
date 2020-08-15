using _9dt.Web.Data;
using _9dt.Web.Dtos;
using _9dt.Web.Exceptions;
using _9dt.Web.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _9dt.Web.Services
{
    public class GameService : IGameService
    {
        private IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public GameService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public IList<Guid> GetInProgressGames()
        {
            var activeGames = _uow.GameInfos.GetActiveGames();
            return activeGames;
        }

        public Guid CreateGame(CreateGameDto request)
        {
            if (request.Rows < 4) throw new BadRequestException("Must have at least 4 rows.");
            if (request.Columns < 4) throw new BadRequestException("Must have at least 4 columns.");
            if (request.Players == null || request.Players.Count < 2) throw new BadRequestException("Must have at least 2 players.");
            if (request.Players.Count() != request.Players.Distinct().Count()) throw new BadRequestException("PlayersId's msut be unique.");

            var rows = request.Rows;
            var columns = request.Columns;
            var gb = new GameBoard(rows, columns);

            var gameId = Guid.NewGuid();
            _uow.Games.AddGame(gameId, gb);

            var info = new GameInfo(request.Players);
            _uow.GameInfos.Add(gameId, info);

            return gameId;
        }

        public GameStateDto GetGameState(Guid gameId)
        {
            var info = _uow.GameInfos.GetGameInfo(gameId);
            if (info == null) throw new NotFoundException("Game not found.");

            var state = new GameStateDto
            {
                Players = info.Players.Select(x => x.Key).ToList(),
                State = info.Status,
                Winner = info.Winner,
            };

            return state;
        }

        public IList<MoveInfoDto> GetMoves(Guid gameId, int? start, int? end)
        {
            var info = _uow.GameInfos.GetGameInfo(gameId);
            if (info == null) throw new NotFoundException("Game not found.");

            if (start == null && end == null)
            {
                var m = _uow.Moves.GetGameMoves(gameId);
                return _mapper.Map<IList<MoveInfoDto>>(m);
            }
                
            
            // TODO: refactor these validation checks..
            if (start == null || end == null) throw new NotFoundException("Moves not found.");  // null-3, 0-null
            if (start < 0 || end < 0) throw new NotFoundException("Moves not found.");  // negative ranges.
            if (start > end) throw new NotFoundException("Moves not found.");  // 3-1, 2-0
            if (end > info.CurrTurn) throw new NotFoundException("Moves not found.");   // 1-4, currTurn is 3.

            var moves = _uow.Moves.GetGameMoves(gameId, start.Value, end.Value);

            return _mapper.Map<IList<MoveInfoDto>>(moves);
        }

        public int MakeMove(Guid gameId, string playerId, int column)
        {
            var gb = _uow.Games.GetGame(gameId);
            if (gb == null) throw new NotFoundException("Game not found.");
            if (column < 0 || column >= gb.GetColumns()) throw new BadRequestException("Illegal move.");

            var info = _uow.GameInfos.GetGameInfo(gameId);
            if (info.Status == GameStatus.DONE) throw new BadRequestException("Game already done.");

            if (!info.PlayerExists(playerId)) throw new NotFoundException("Player is not part of the game.");

            if (info.GetCurrentPlayer() != playerId) throw new ConflictException("Not player's turn.");

            var playerToken = info.GetPlayerToken(playerId);
            var dropStatus = gb.MakeMove(playerToken, column);

            if (dropStatus == DropStatus.COLUMN_FULL) throw new BadRequestException("Column is full.");

            info.NextTurn();
            var move = new MoveInfo
            {
                Column = column,
                Type = MoveType.MOVE,
                Player = playerId
            };
            var move_number = _uow.Moves.AddMove(gameId, move);

            // handle win/draw.
            if (dropStatus == DropStatus.WIN)
            {
                info.Status = GameStatus.DONE;
                info.Winner = playerId;
            }
            else if (dropStatus == DropStatus.DRAW)
            {
                info.Status = GameStatus.DONE;
                info.Winner = null;
            }

            return move_number;
        }

        public MoveInfoDto GetMove(Guid gameId, int move_number)
        {
            var info = _uow.GameInfos.GetGameInfo(gameId);
            if (info == null) throw new NotFoundException("Game not found.");

            if (move_number > info.CurrTurn) throw new NotFoundException("Move not found.");   // 4 when currTurn is 3.

            var move = _uow.Moves.GetGameMove(gameId, move_number);
            return _mapper.Map<MoveInfoDto>(move);
        }

        public void QuitGame(Guid gameId, string playerId)
        {
            var info = _uow.GameInfos.GetGameInfo(gameId);
            if (info == null) throw new NotFoundException("Game not found.");
            
            if (info.Status == GameStatus.DONE) throw new GoneException("Game is already in DONE state.");

            if (!info.PlayerExists(playerId)) throw new NotFoundException("Player is not part of the game.");

            info.Quit(playerId);
            var move = new MoveInfo
            {
                Type = MoveType.QUIT,
                Player = playerId
            };
            _uow.Moves.AddMove(gameId, move);   // record quit move.
        }
    }
}
