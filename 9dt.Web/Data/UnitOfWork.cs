using _9dt.Web.Repositories;
using _9dt.Web.Repositories.Interfaces;

namespace _9dt.Web.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private IGameRepository _gameRepository;
        private IGameInfoRepository _gameInfoRepository;
        private IMoveRepository _moveRepository;
        private readonly Context _context;

        public UnitOfWork(Context context)
        {
            _context = context;
        }

        public IGameRepository Games => _gameRepository = _gameRepository ?? new GameRepository(_context);
        public IGameInfoRepository GameInfos => _gameInfoRepository = _gameInfoRepository ?? new GameInfoRepository(_context);
        public IMoveRepository Moves => _moveRepository = _moveRepository ?? new MoveRepository(_context);
    }
}
