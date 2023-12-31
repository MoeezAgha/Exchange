using Exchange.BAL.Services.Contracts;
using Exchange.DAL;
using Exchange.DAL.Models;

namespace Exchange.BAL.Services.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }

    }
}
