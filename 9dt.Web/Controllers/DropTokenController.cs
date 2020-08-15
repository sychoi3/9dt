using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using _9dt.Web.Data;
using _9dt.Web.Dtos;
using _9dt.Web.Models;
using _9dt.Web.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace _9dt.Web.Controllers
{
    [Route("api/drop_token")]
    [ApiController]
    public class DropTokenController : ControllerBase
    {
        private IGameService _service;
        private IMapper _mapper;
        private ILogger _logger;
        public DropTokenController(IGameService service, IMapper mapper, ILogger<DropTokenController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Return all in-progress games.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetGames()
        {
            _logger.LogInformation("GetGames called.");
            var gameIds = _service.GetInProgressGames();
            var result = _mapper.Map<GetGamesResponse>(gameIds);
            return Ok(result);
        }

        /// <summary>
        /// Create a new game.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(CreateGameRequest request)
        {
            var newGame = _mapper.Map<CreateGameDto>(request);

            var gameId = _service.CreateGame(newGame);
            var response = _mapper.Map<CreateGameResponse>(gameId);
            return Ok(response);
        }

        /// <summary>
        /// Get the state of the game.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{gameId}")]
        public IActionResult Get(Guid gameId)
        {
            var state = _service.GetGameState(gameId);

            if (state == null) return NotFound();   // throw NotFoundException instead.

            var options = new JsonSerializerOptions();
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.Converters.Add(new JsonStringEnumConverter());
            if (state.State == GameStatus.IN_PROGRESS)
                options.IgnoreNullValues = true;    // No Winner key.

            var jsonStr = JsonSerializer.Serialize(state, options);
            return Ok(jsonStr);
        }

        /// <summary>
        /// Get (sub) list of the moves played.
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="start"></param>
        /// <param name="until"></param>
        /// <returns></returns>
        [HttpGet("{gameId}/moves")]
        public IActionResult GetMoves(Guid gameId, int? start, int? until)
        {
            var moves = _service.GetMoves(gameId, start, until);
            var resource = _mapper.Map<GameMovesResource>(moves);
            return Ok(resource);
        }

        /// <summary>
        /// Post a move.
        /// </summary>
        [HttpPost("{gameId}/{playerId}")]
        public IActionResult MakeMove(Guid gameId, string playerId, MakeMoveRequest request)
        {
            var move_number = _service.MakeMove(gameId, playerId, request.column);
            var response = new MakeMoveResponse($"{gameId}/moves/{move_number}");
            return Ok(response);
        }

        /// <summary>
        /// Return the move.
        /// </summary>
        [HttpGet("{gameId}/moves/{move_number}")]
        public IActionResult GetMove(Guid gameId, int move_number)
        {
            var move = _service.GetMove(gameId, move_number);
            return Ok(move);
        }

        /// <summary>
        /// Player quits from game.
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="playerId"></param>
        [HttpDelete("{gameId}/{playerId}")]
        public IActionResult Delete(Guid gameId, string playerId)
        {
            _service.QuitGame(gameId, playerId);
            return Accepted();
        }
    }
}
