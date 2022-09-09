using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ordering.Domain.Entities.Base;

namespace Ordering.Domain.Repositories.Base
{
    public interface IRepository<TEntity> where TEntity : class, IEntity,new()
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter);
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter,Func<IQueryable<TEntity>,IOrderedQueryable<TEntity>> orderBy=null,string includeString=null,bool disableTracking=true);
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
