using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokiMani.Core.DTOs;
using PokiMani.Core.Entities;
using PokiMani.Core.Interfaces.IServices;
using PokiMani.Core.Interfaces.IServicesInfrastructure;
using System.Threading.Tasks;

namespace PokiMani.Api.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Account>))]        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAllAccounts()
        {
            var result = await _accountService.GetUserAccountsAsync();
            return Ok (result);    
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Account))]
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<Account>> GetAccountById(Guid id)
        {
            var result = await _accountService.GetAccountByIdAsync(id);
            return Ok (result); 
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Account))]
        [HttpPost]
        public async Task<ActionResult<Account>> CreateNewAccount(AccountDto dto)
        {
            var result = await _accountService.CreateAccountAsync(dto);
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Account))]
        [HttpPatch("{id:Guid}")]
        public IActionResult UpdateNewAccount(Guid id, AccountDto dto)
        {
            return StatusCode(503, "Not ImplementedYet");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteAccount(Guid id)
        {
            await _accountService.DeleteAccountAsync(id);
            return Ok();
        }
    }
}
