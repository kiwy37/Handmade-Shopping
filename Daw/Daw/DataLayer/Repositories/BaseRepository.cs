
using Daw.DataLayer.DataBaseConenction;
using Daw.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Daw.DataLayer.Repositories
{
    public class BaseRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _appContext;
        protected readonly DbSet<T> _dbSet;
        public BaseRepository(AppDbContext appContext)
        {
            _appContext = appContext;
            _dbSet = _appContext.Set<T>();
        }
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task<T> AddAsync(T entity)
        {
            if(_dbSet is null)
            {
                return null;
            }
            await _dbSet.AddAsync(entity);
            await _appContext.SaveChangesAsync();
            return entity;
        }
        public async Task<T> UpdateAsync(T entity)
        {
            if(_dbSet is null)
            {
                return null;
            }
            _dbSet.Update(entity);
            await _appContext.SaveChangesAsync();
            return entity;
        }
        public async Task<T?> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);

            if (entity is null) return null;

            _dbSet.Remove(entity);
            await _appContext.SaveChangesAsync();
            return entity;
        }

    }
}
