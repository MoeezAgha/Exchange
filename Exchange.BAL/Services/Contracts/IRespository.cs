using Exchange.BAL.Services.Repositories;
using Exchange.BAL.Services.ResponseWrapperService;
using Exchange.DAL.Models;
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
    public interface ICategoryRepository : IRepository<Category>
    {
        // Additional methods specific to Category
    }

    public interface IExchangeOfferRepository : IRepository<ExchangeOffer>
    {
        // Additional methods specific to ExchangeOffer
    }

    public interface IImageRepository : IRepository<Image>
    {
        // Additional methods specific to Image
    }

    public interface IProductRepository : IRepository<Product>
    {
        // Additional methods specific to Product
    }

    public interface IProductImageRepository : IRepository<ProductImage>
    {
        // Additional methods specific to ProductImage
    }

    public interface ITagRepository : IRepository<Tag>
    {
        // Additional methods specific to Tag
    }
  
}
