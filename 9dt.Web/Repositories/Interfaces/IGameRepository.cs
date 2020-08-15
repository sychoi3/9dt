using _9dt.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _9dt.Web.Repositories.Interfaces
{
    public interface IGameRepository
    {
        void AddGame(Guid gameId, GameBoard gb);
        GameBoard GetGame(Guid gameId);
    }
}
