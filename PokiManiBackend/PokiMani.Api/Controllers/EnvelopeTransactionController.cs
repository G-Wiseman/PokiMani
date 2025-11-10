using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokiMani.Core.DTOs;
using PokiMani.Core.Entities;
using PokiMani.Core.Interfaces.IServices;
using System.Threading.Tasks;

namespace PokiMani.Api.Controllers
{
    [Route("api/envelope-transactions")]
    [ApiController]
    public class EnvelopeTransactionController : ControllerBase
    {
        IEnvelopeTransactionService _envelopeTransactionService;
        public EnvelopeTransactionController(IEnvelopeTransactionService envelopeTransactionService)
        {
            _envelopeTransactionService = envelopeTransactionService;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EnvelopeTransaction>))]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnvelopeTransaction>>> GetAllEnvelopeTransactions()
        {
            var result = await _envelopeTransactionService.GetUserEnvelopeTransactionsAsync();
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EnvelopeTransaction))]
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<EnvelopeTransaction>> GetEnvelopeTransactionById(Guid id)
        {
            var result = await _envelopeTransactionService.GetEnvelopeTransactionByIdAsync(id);
            return Ok(result);
        }


        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EnvelopeTransaction))]
        [HttpPost]
        public async Task<ActionResult<Envelope>> CreateNewEnvelopeTransaction(CreateEnvelopeTransactionDto dto)
        {
            var result = await _envelopeTransactionService.CreateEnvelopeTransactionAsync(dto);
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EnvelopeTransaction))]
        [HttpPatch("{id:Guid}")]
        public IActionResult UpdateNewEnvelopeTransaction(Guid id, EnvelopeTransactionDto dto)
        {
            return StatusCode(503, "Not ImplementedYet");
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteEnvelopeTransaction(Guid id)
        {
            await _envelopeTransactionService.DeleteEnvelopeTransactionAsync(id);
            return Ok();
        }
    }
}
