﻿using Microsoft.EntityFrameworkCore;
using MachineHealthCheck.Domain.Interfaces;
using System.Linq.Expressions;

namespace MachineHealthCheck.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public DbSet<T> Entities => DbContext.Set<T>();
        public DbContext DbContext { get; private set; }
        public Repository(DbContext dbContext)
        {
            DbContext = dbContext;
        }
        public async Task DeleteAsync(int id, bool saveChanges = true)
        {
            var entity = await Entities.FindAsync(id);
            await DeleteAsync(entity);
            if (saveChanges)
            {
                await DbContext.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync(T entity, bool saveChanges = true)
        {
            Entities.Remove(entity);
            if (saveChanges)
            {
                await DbContext.SaveChangesAsync();
            }
        }
        public async Task<IQueryable<T>> FindByCondition(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            var query = Entities.Where(expression).AsNoTracking();
            return includes.Aggregate(query, (q, path) => q.Include(path));
        }
        public async Task DeleteRangeAsync(IEnumerable<T> entities, bool saveChanges = true)
        {
            var enumerable = entities as T[] ?? entities.ToArray();
            if (enumerable.Any())
            {
                Entities.RemoveRange(enumerable);
            }

            if (saveChanges)
            {
                await DbContext.SaveChangesAsync();
            }
        }
        public async Task<IList<T>> GetAllAsync()
        {
            return await Entities.ToListAsync();
        }
        public T Find(params object[] keyValues)
        {
            return Entities.Find(keyValues);
        }
        public virtual async Task<T> FindAsync(params object[] keyValues)
        {
            return await Entities.FindAsync(keyValues);
        }
        public async Task InsertAsync(T entity, bool saveChanges = true)
        {
            await Entities.AddAsync(entity);

            if (saveChanges)
            {
                await DbContext.SaveChangesAsync();
            }
        }
        public async Task InsertRangeAsync(IEnumerable<T> entities, bool saveChanges = true)
        {
            await DbContext.AddRangeAsync(entities);
            if (saveChanges)
            {
                await DbContext.SaveChangesAsync();
            }
        }
    }
}

