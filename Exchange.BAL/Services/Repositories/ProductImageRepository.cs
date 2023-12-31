using Exchange.BAL.Services.Contracts;
using Exchange.DAL;
using Exchange.DAL.Models;

namespace Exchange.BAL.Services.Repositories
{
    public class ProductImageRepository : Repository<ProductImage>, IProductImageRepository
    {
        public ProductImageRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
