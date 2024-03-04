using Exchange.BAL.Services.Contracts;

using Exchange.DAL.Models;
using Exchange.Library.DataTransferObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exchange.WebAPI.Controllers
{
    //  [Authorize(Roles = "aa")]
   // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IRepository<Product> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IRepository<Product> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

  
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get([FromQuery] bool includeProducts = false)
        {
            var product = includeProducts
              ? await _repository.GetAllIncludingAsync(tag => tag.Tags, Category => Category.Category )
              : await _repository.GetAllAsync();
            return Ok(product);
        }

   

        // POST api/Product
        [HttpPost]
        public async Task<ActionResult<ProductDTO>> Post([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model state.");
            }

            await _repository.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = product.ProductId }, product);
        }

        // PUT api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest("Invalid ID");
            }

            await _repository.UpdateAsync(product);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            product.IsPublic = false;

            await _repository.DeleteAsync(product);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}
