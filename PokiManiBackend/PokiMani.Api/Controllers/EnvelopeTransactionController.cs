using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokiMani.Core.DTOs;
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

        [HttpGet]
        public async Task<IActionResult> GetAllEnvelopeTransactions()
        {
            var result = await _envelopeTransactionService.GetUserEnvelopeTransactionsAsync();
            return Ok(result);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetEnvelopeTransactionById(Guid id)
        {
            var result = await _envelopeTransactionService.GetEnvelopeTransactionByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewEnvelopeTransaction(CreateEnvelopeTransactionDto dto)
        {
            var result = await _envelopeTransactionService.CreateEnvelopeTransactionAsync(dto);
            return Ok(result);
        }

        [HttpPatch("{id:Guid}")]
        public IActionResult UpdateNewEnvelopeTransaction(Guid id, EnvelopeTransactionDto dto)
        {
            return StatusCode(503, "Not ImplementedYet");
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteEnvelopeTransaction(Guid id)
        {
            await _envelopeTransactionService.DeleteEnvelopeTransactionAsync(id);
            return Ok();
        }
    }
}
