using _9dt.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _9dt.Web.Repositories.Interfaces
{
    public interface IGameInfoRepository
    {
        void Add(Guid gameId, GameInfo info);
        IList<Guid> GetActiveGames();
        GameInfo GetGameInfo(Guid gameId);
    }
}
