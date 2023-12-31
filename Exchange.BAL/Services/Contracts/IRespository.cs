using Exchange.BAL.Services.Repositories;
using Exchange.BAL.Services.ResponseWrapperService;
using Exchange.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.BAL.Services.Contracts
{
    public interface IRepository<TEntity> where TEntity : class
    {
      
        // Get all entities
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IQueryable<TEntity>> GetAllIncludingAsync(params Expression<Func<TEntity, object>>[] includeProperties);

        // Get entities that match a specified criteria
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);

        // Get an entity by its identifier
        Task<TEntity> GetByIdAsync(object id);

        // Add a new entity
        Task AddAsync(TEntity entity);

        // Update an existing entity
        Task UpdateAsync(TEntity entity);

        // Delete an entity
        Task DeleteAsync(TEntity entity);
}
  
}
