using Exchange.BAL.Services.Contracts;
using Exchange.BAL.Services.Repositories;

using Exchange.DAL.Models;
using Exchange.Library.DataTransferObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace Exchange.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
 //   [Authorize]
    public class CategoryController(IUnitOfWork unitOfWork, IServiceProvider serviceProvider, AutoMapper.IMapper mapper) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        private readonly IServiceProvider _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        private readonly AutoMapper.IMapper _mapper = mapper;

  

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExchangeOffer>>> Get([FromQuery] bool includeProducts = false)
        {
 
           
            var exchangeOffers = includeProducts
              ? await _unitOfWork.Categories.GetAllIncludingAsync(c => c.Products)
              : await _unitOfWork.Categories.GetAllAsync();


            return Ok(exchangeOffers);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            return category != null ? Ok(category) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> Post([FromBody] CategoryDTO category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model state.");
            }
         
            await _unitOfWork.Categories.AddAsync(_mapper.Map<Category>(category));
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = category.CategoryId }, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CategoryDTO category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest("Invalid ID");
            }

            await _unitOfWork.Categories.UpdateAsync(_mapper.Map<Category>(category));
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            category.IsPublic = false;
            await _unitOfWork.Categories.UpdateAsync(category);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}
