using Exchange.BAL.Services.Contracts;
using Exchange.DAL;
using Exchange.DAL.Models;

namespace Exchange.BAL.Services.Repositories
{
    public class ImageRepository : Repository<Image>, IImageRepository
    {
        public ImageRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
