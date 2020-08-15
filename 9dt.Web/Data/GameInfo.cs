using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _9dt.Web.Data
{
    public class GameInfo
    {
        public GameStatus Status { get; set; }
        public string Winner { get; set; }   // null if draw. or in-progress.
        public Dictionary<string, int> Players { get; set; }
        private int PlayerCount { get; set; }
        public int CurrTurn { get; set; }
        private IList<string> ActivePlayers { get; set; }

        public GameInfo(IList<string> players)
        {
            ActivePlayers = new List<string>();
            Players = new Dictionary<string, int>();
            for (int i = 0; i < players.Count; i++)
            {
                ActivePlayers.Add(players[i]);
                Players.Add(players[i], i + 1); // [playerId:playerToken]
            }
            CurrTurn = 0;
            PlayerCount = players.Count;
            Status = GameStatus.IN_PROGRESS;
            Winner = null;
        }

        public bool PlayerExists(string playerId)
        {
            return ActivePlayers.Contains(playerId);
        }

        public int GetPlayerToken(string playerId)
        {
            if (Players.ContainsKey(playerId))
                return Players[playerId];

            throw new Exception("Player not found.");
        }

        public void NextTurn()
        {
            CurrTurn++;
            if (CurrTurn >= ActivePlayers.Count) CurrTurn = 0;  // back to player 1.
        }

        public string GetCurrentPlayer()
        {
            return ActivePlayers[CurrTurn];
        }

        public void Quit(string playerId)
        {
            // drop player
            // if only 1 player remaining, that player is winner.

            ActivePlayers.Remove(playerId);
            if(ActivePlayers.Count == 1)
            {
                Status = GameStatus.DONE;
                Winner = ActivePlayers[0];
            }
        }
    }
}
