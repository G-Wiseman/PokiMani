using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokiMani.Core.DTOs;
using PokiMani.Core.Entities;
using PokiMani.Core.Interfaces.IServices;
using PokiMani.Core.Interfaces.IServicesInfrastructure;
using PokiMani.Infrastructure.Data;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Swagger;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PokiMani.Api.Controllers
{
    [Authorize]
    [Route("api/envelopes")]
    [ApiController]
    public class EnvelopeController: ControllerBase
    {
        IEnvelopeService _envelopeService;

        public EnvelopeController(IEnvelopeService envelopeService)
        {
            _envelopeService = envelopeService;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Envelope>))]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Envelope>>> getAllEnvelopes()
        {
            var result = await _envelopeService.GetUserEnvelopesAsync();
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK,Type= typeof(Envelope))]
        [HttpPost]
        public async Task<ActionResult<Envelope>> AddEnvelope(EnvelopeDto dto)
        {
            var result = await _envelopeService.CreateEnvelopeAsync(dto);
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Envelope))]
        [HttpGet("{id:Guid}")] 
        public async Task<ActionResult<Envelope>> GetSingleEnvelope(Guid id)
        {
            var result = await _envelopeService.GetEnvelopeByIdAsync(id);
            return Ok(result);
        }

        [HttpPatch("{id:Guid}")]
        public IActionResult UpdateSingleEnvelope(Guid id, EnvelopeDto dto)
        {
            return StatusCode(503, "Not Implemented Yet");
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteSingleEnvelope(Guid id)
        {
            await _envelopeService.DeleteEnvelopeAsync(id);
            return NoContent();
        }
    }
}
