using Exchange.DAL.Models;

namespace Exchange.BAL.Services.Contracts
{
    public interface IExchangeOfferRepository : IRepository<ExchangeOffer>
    {
        // Additional methods specific to ExchangeOffer
        string GetEx();
    }
  
}
