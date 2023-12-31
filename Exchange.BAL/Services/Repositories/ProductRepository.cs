using Exchange.BAL.Services.Contracts;
using Exchange.DAL;
using Exchange.DAL.Models;

namespace Exchange.BAL.Services.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
