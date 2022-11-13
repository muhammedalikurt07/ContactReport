using Entity.Base;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Dal.Abstractions
{
    public interface IRepository<T> where T : EntityBase
    {
        ValueTask<EntityEntry<T>> AddAsync(T _entity);
        Task AddAsync(IEnumerable<T> _entities);
        void UpdateAsync(IEnumerable<T> _entities);
        void UpdateAsync(T entity);
        Task<T> SingleAsync(Expression<Func<T, bool>> predicate = null,
                            Func<IQueryable<T>,
                            IIncludableQueryable<T, object>> include = null,
                            bool disableTracking = true);
        Task<List<T>> GetAllInclude(Expression<Func<T, bool>> predicate = null,
                                          Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                                          bool disableTracking = false);
    }
}
