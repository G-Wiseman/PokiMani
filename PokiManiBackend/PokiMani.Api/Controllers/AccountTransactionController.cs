using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokiMani.Core.DTOs;
using PokiMani.Core.Entities;
using PokiMani.Core.Interfaces.IServices;


namespace PokiMani.Api.Controllers
{
    [Route("api/account-transactions")]
    [ApiController]
    [Authorize]
    public class AccountTransactionController : ControllerBase
    {
        IAccountTransactionService _accountTransactionService;
        public AccountTransactionController(IAccountTransactionService accountTransactionService) 
        {
            _accountTransactionService = accountTransactionService;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type= typeof(IEnumerable<AccountTransaction>))]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountTransaction>>> GetAllAccountTransactions()
        {
            var result = await _accountTransactionService.GetUserAccountTransactionsAsync();
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AccountTransaction))]
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<AccountTransaction>> GetAccountTransactionById(Guid id)
        {
            var result = await _accountTransactionService.GetAccountTransactionByIdAsync(id);
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AccountTransaction))]
        [HttpPost]
        public async Task<ActionResult<AccountTransaction>> CreateNewAccountTransaction(CreateAccountTransactionDto dto)
        {
            var result = await _accountTransactionService.CreateAccountTransactionAsync(dto);
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AccountTransaction))]
        [HttpPatch("{id:Guid}")]
        public IActionResult UpdateNewAccountTransaction(Guid id, AccountTransactionDto dto)
        {
            return StatusCode(503, "Not ImplementedYet");
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteAccountTransaction(Guid id)
        {
            await _accountTransactionService.DeleteAccountTransactionAsync(id);
            return Ok();
        }
    }
}
