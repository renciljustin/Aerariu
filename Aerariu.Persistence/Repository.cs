using Aerariu.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Aerariu.Persistence
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AerariuDbContext _dbContext;
        protected readonly DbSet<T> _entitySet;

        public Repository(AerariuDbContext dbContext)
        {
            _dbContext = dbContext;
            _entitySet = _dbContext.Set<T>();
        }

        public void Add(T entity)
            => _entitySet.Add(entity);

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
            => await _entitySet.AddAsync(entity, cancellationToken);

        public void AddRange(IEnumerable<T> entities)
            => _entitySet.AddRange(entities);

        public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
            => await _entitySet.AddRangeAsync(entities, cancellationToken);

        public T Get(Expression<Func<T, bool>> expression)
            => _entitySet.FirstOrDefault(expression);

        public IEnumerable<T> GetAll()
            => _entitySet.AsEnumerable();

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> expression)
            => _entitySet.Where(expression).AsEnumerable();

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _entitySet.ToListAsync(cancellationToken);

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
            => await _entitySet.Where(expression).ToListAsync(cancellationToken);

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
            => await _entitySet.FirstOrDefaultAsync(expression, cancellationToken);

        public void Remove(T entity)
            => _entitySet.Remove(entity);

        public void RemoveRange(IEnumerable<T> entities)
            => _entitySet.RemoveRange(entities);

        public void Update(T entity)
            => _entitySet.Update(entity);

        public void UpdateRange(IEnumerable<T> entities)
            => _entitySet.UpdateRange(entities);
    }
}
