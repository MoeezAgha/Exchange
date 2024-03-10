using AutoMapper;
using Exchange.BAL.Services.Contracts;
using Exchange.DAL.Models;
using Exchange.Library.DataTransferObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exchange.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
 //   [Authorize]
    public class TagController : ControllerBase
    {
        private readonly IRepository<Tag> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TagController(IRepository<Tag> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tag>>> Get([FromQuery] bool includeProducts = false)
        {
              var tags = includeProducts 
                ? await _repository.GetAllIncludingAsync(c=>c.Products)
                : await _repository.GetAllAsync();


            return Ok(tags);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tag>> GetId(int id)
        {
            var tag = await _repository.GetByIdAsync(id);
            return tag != null ? Ok(tag) : NotFound();
        }
        [HttpPost]
        public async Task<ActionResult<Tag>> Post([FromBody] Tag tag)
        {
            if (!ModelState.IsValid)
            {
                // Consider returning more detailed validation errors
                return BadRequest(ModelState);
            }

            await _repository.AddAsync(tag);
            await _unitOfWork.SaveChangesAsync();
           

            // Return a 201 Created response with a location header pointing to the new resource
            return CreatedAtAction(nameof(GetId), new { id = tag.TagId }, _mapper.Map<TagDTO>(tag));
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Tag tag)
        {
            if (id != tag.TagId)
            {
                return BadRequest("Invalid ID");
            }

            await _repository.UpdateAsync(tag);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var tag = await _repository.GetByIdAsync(id);
            if (tag == null)
            {
                return NotFound();
            }
            tag.IsPublic = false;
            await _repository.DeleteAsync(tag);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}
