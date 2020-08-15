using _9dt.Web.Repositories;
using _9dt.Web.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _9dt.Web.Data
{
    public interface IUnitOfWork
    {
        IGameRepository Games { get; }
        IGameInfoRepository GameInfos { get; }
        IMoveRepository Moves { get; }
    }
}
