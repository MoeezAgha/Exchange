using Exchange.BAL.Services.Contracts;
using Exchange.DAL;
using Exchange.DAL.Models;

namespace Exchange.BAL.Services.Repositories
{
    public class ExchangeOfferRepository : Repository<ExchangeOffer>, IExchangeOfferRepository
    {
        public ExchangeOfferRepository(ApplicationDbContext context) : base(context)
        {
        }

    }
}
