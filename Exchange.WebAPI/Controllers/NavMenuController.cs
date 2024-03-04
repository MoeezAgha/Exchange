using Exchange.BAL.Services.Contracts;
using Exchange.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Exchange.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NavMenuController : ControllerBase
    {
        private readonly IRepository<NavMenu> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public NavMenuController(IRepository<NavMenu> repository, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _userManager = userManager;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {

            var user = await _userManager.GetUserAsync(this.User);
            var z = await _userManager.GetRolesAsync(user);
            var zb = z;
            var zs = await _unitOfWork.NavMenu.GetAllMenuWIthRole(z.ToList());
            return Ok(zs);
        }
    }
}
