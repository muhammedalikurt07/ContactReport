using Dal.Abstractions;
using Entity.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Dal.Concretes
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        private readonly DbContext _Context;

        private DbSet<T> _Entities;
        public Repository(DbContext _context)
        {
            _Context = _context;

            _Entities = _context.Set<T>();
        }

        public ValueTask<EntityEntry<T>> AddAsync(T _entity) => _Entities.AddAsync(_entity);
        public Task AddAsync(IEnumerable<T> _entities) => _Entities.AddRangeAsync(_entities);
        public void UpdateAsync(IEnumerable<T> _entities) => _Entities.UpdateRange(_entities);
        public void UpdateAsync(T entity) => _Entities.Update(entity);
        public async Task<T> SingleAsync(Expression<Func<T, bool>> predicate = null,
                                         Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                                         bool disableTracking = true)
        {
            IQueryable<T> query = _Entities;
            if (disableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            return await query.FirstOrDefaultAsync();
        }

        public Task<List<T>> GetAllInclude(Expression<Func<T, bool>> predicate = null,
                                          Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                                          bool disableTracking = false)
        {
            IQueryable<T> query = _Entities;
            if (disableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            return query.ToListAsync();
        }

    }
}
