using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokiMani.Core.DTOs;
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

        [HttpGet]
        public async Task<IActionResult> GetAllAccounts()
        {
            var result = await _accountService.GetUserAccountsAsync();
            return Ok (result);    
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetAccountById(Guid id)
        {
            var result = await _accountService.GetAccountByIdAsync(id);
            return Ok (result); 
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewAccount(AccountDto dto)
        {
            var result = await _accountService.CreateAccountAsync(dto);
            return Ok(result);
        }

        [HttpPatch("{id:Guid}")]
        public IActionResult UpdateNewAccount(Guid id, AccountDto dto)
        {
            return StatusCode(503, "Not ImplementedYet");
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteAccount(Guid id)
        {
            await _accountService.DeleteAccountAsync(id);
            return Ok();
        }
    }
}
