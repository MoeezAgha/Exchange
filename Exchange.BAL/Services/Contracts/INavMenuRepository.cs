using Exchange.DAL.Models;
using Microsoft.AspNetCore.Identity;

namespace Exchange.BAL.Services.Contracts
{
    public interface INavMenuRepository : IRepository<NavMenu>
    {
         Task<IEnumerable<NavMenu>> GetAllMenuWIthRole(List<string> identityRoleid);
    }

  
}
