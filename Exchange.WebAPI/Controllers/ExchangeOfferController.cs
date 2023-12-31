using Exchange.BAL.Services.Contracts;
using Exchange.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exchange.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class ExchangeOfferController : ControllerBase
    {
        private readonly IRepository<ExchangeOffer> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ExchangeOfferController(IRepository<ExchangeOffer> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExchangeOffer>>> Get()
        {
            var exchangeOffers = await _repository.GetAllAsync();
            return Ok(exchangeOffers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExchangeOffer>> Get(int id)
        {
            var exchangeOffer = await _repository.GetByIdAsync(id);
            return exchangeOffer != null ? Ok(exchangeOffer) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<ExchangeOffer>> Post([FromBody] ExchangeOffer exchangeOffer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model state.");
            }

            await _repository.AddAsync(exchangeOffer);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = exchangeOffer.ExchangeOfferId }, exchangeOffer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ExchangeOffer exchangeOffer)
        {
            if (id != exchangeOffer.ExchangeOfferId)
            {
                return BadRequest("Invalid ID");
            }

            await _repository.UpdateAsync(exchangeOffer);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var exchangeOffer = await _repository.GetByIdAsync(id);
            if (exchangeOffer == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(exchangeOffer);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}
