using Infrastucture.Repositories;
using Application.Common.Interfaces.Repositories;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence;

namespace Infrastucture.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly DbSet<T> _table;
        protected readonly ApplicationDbContext _dbContext;

        public BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _table = _dbContext.Set<T>();
        }

        public void Insert(T entity)
        {
            _table.Add(entity);
        }

        public void Update(T entity)
        {
            _table.Update(entity);
        }

        public void Delete(T entity)
        {
            entity.Deleted = true;
            _table.Update(entity);
            this.SaveChanges();
        }

        public void InsertMany(IEnumerable<T> entities)
        {
            _table.AddRange(entities);
            this.SaveChanges();
        }

        public virtual async Task<List<T>> GetAsync()
        {
            return await _table.ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            return await _table.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
        public bool SaveChanges()
        {
            return _dbContext.SaveChanges() > 0;
        }
    }
}