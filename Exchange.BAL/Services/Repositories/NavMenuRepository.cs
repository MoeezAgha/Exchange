using Exchange.BAL.Services.Contracts;
using Exchange.DAL;
using Exchange.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Exchange.BAL.Services.Repositories
{
    public class NavMenuRepository : Repository<NavMenu>, INavMenuRepository
    {
        ApplicationDbContext applicationDbContext;
        public NavMenuRepository(ApplicationDbContext context) : base(context)
        {
            applicationDbContext = context;
        }
        public async Task<IEnumerable<NavMenu>> GetAllMenuWIthRole(List<string> identityRoleid) {

            var userRole = await applicationDbContext.Roles.Where(c=>identityRoleid.Contains(c.Name.ToString())).FirstOrDefaultAsync();
            var menu = await applicationDbContext.NavMenu.Where(c=>c.AccessRoleId.Id == userRole.Id).ToListAsync();
            return menu;
          
       


        }

    }
}
