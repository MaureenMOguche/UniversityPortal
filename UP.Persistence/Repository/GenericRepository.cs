using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UP.Application.Contracts.Persistence;
using UP.Domain;

namespace UP.Persistence.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly UPDbContext _db;
        private DbSet<T> _dbSet;
        public GenericRepository(UPDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
        }
        public async Task<IEnumerable<T>?> GetAllAsync(string? includeproperties = null, Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeproperties != null)
            {
                foreach (var property in includeproperties.Split(new[]{','}))
                {
                    query = query.Include(property);
                }
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetOneAsync(Expression<Func<T, bool>> filter, string? includeproperties = null)
        {
            IQueryable<T> query = _dbSet;
            query = query.Where(filter);

            if (includeproperties != null)
            {
                foreach (var property in includeproperties.Split(new []{','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }

            return await query.AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<T> CreateAsync(T entity)
        {
            var result = await _dbSet.AddAsync(entity);
            await _db.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<int> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return await _db.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            return await _db.SaveChangesAsync();
        }

        public async Task<int> BulkDeleteAsync(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
            return await _db.SaveChangesAsync();
        }
        
        public async Task<int> BulkAdd(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            return await _db.SaveChangesAsync();
        }
    }
}
