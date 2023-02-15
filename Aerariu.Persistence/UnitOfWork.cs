using Aerariu.Core;
using Aerariu.Core.Repositories;
using Aerariu.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerariu.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AerariuDbContext _dbContext;
        private IUserRepository _userRepository;

        public UnitOfWork(AerariuDbContext dbContext) {
            _dbContext = dbContext;
        }

        public IUserRepository UserRepository => _userRepository ?? new UserRepository(_dbContext);

        public void Commit()
            => _dbContext.SaveChanges();

        public async Task CommitAsync()
            => await _dbContext.SaveChangesAsync();

        public void Rollback()
            => _dbContext.Dispose();

        public async Task RollbackAsync()
            => await _dbContext.DisposeAsync();
    }
}
