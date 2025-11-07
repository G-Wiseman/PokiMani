using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokiMani.Core.DTOs;
using PokiMani.Core.Entities;
using PokiMani.Core.Interfaces.IServices;
using PokiMani.Core.Interfaces.IServicesInfrastructure;
using PokiMani.Infrastructure.Data;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PokiMani.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EnvelopesController: ControllerBase
    {
        IEnvelopeService _envelopeService;

        public EnvelopesController(IEnvelopeService envelopeService)
        {
            _envelopeService = envelopeService;
        }

        [HttpGet]
        public async Task<IActionResult> getAllEnvelopes()
        {
            var result = await _envelopeService.GetUserEnvelopesAsync();
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> AddEnvelope(EnvelopeDto dto)
        {
            var result = await _envelopeService.CreateEnvelopeAsync(dto);
            return Ok(result);
        }


        [HttpGet("{id:Guid}")] 
        public async Task<IActionResult> GetSingleEnvelope(Guid id)
        {
            var result = await _envelopeService.GetEnvelopeByIdAsync(id);
            return Ok(result);
        }

        [HttpPatch("{id:Guid}")]
        public IActionResult UpdateSingleEnvelope(Guid id, EnvelopeDto dto)
        {
            return StatusCode(503, "Not Implemented Yet");
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteSingleEnvelope(Guid id)
        {
            await _envelopeService.DeleteEnvelopeAsync(id);
            return Ok();
        }
    }
}
