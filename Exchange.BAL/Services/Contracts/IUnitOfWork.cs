using Exchange.BAL.Services.Repositories;
using Exchange.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.BAL.Services.Contracts
{
    public interface IUnitOfWork:IDisposable
    {
         CategoryRepository Categories { get;  }
         ExchangeOfferRepository ExchangeOffers { get;  }
         ImageRepository Images { get;  }
         ProductRepository Products { get;  }
         ProductImageRepository ProductImages { get;  }
         TagRepository Tags { get;  }
        NavMenuRepository NavMenu { get;  }
         Task<int> SaveChangesAsync();
     
    }

}
