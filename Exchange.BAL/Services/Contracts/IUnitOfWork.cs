using Exchange.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.BAL.Services.Contracts
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }

}
