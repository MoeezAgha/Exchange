using Exchange.BAL.Services.Contracts;
using Exchange.DAL;
using Exchange.DAL.Models;

namespace Exchange.BAL.Services.Repositories
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        public TagRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
