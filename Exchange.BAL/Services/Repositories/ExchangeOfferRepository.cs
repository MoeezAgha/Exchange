using Exchange.BAL.Services.Contracts;
using Exchange.DAL;
using Exchange.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Exchange.BAL.Services.Repositories
{
    public class ExchangeOfferRepository : Repository<ExchangeOffer>, IExchangeOfferRepository
    {
        private readonly ApplicationDbContext _context;
        public ExchangeOfferRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public string GetEx() {

          var o=  _context.Products.Include(c => c.AcceptedOffer).Include(c => c.ExchangeOffers).ToList();
            var b = _context.ExchangeOffer.Where(c => c.ProductId != 0).ToList() ;
            var z = _context.Products.Where(c => c.AcceptedOfferId != null);
            return "";
        }

    }
}
