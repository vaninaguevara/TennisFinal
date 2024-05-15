using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tennis.Models;
using Tennis.Repository;
using Tennis.Services.Interfaces;

namespace Tennis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TorneoController : ControllerBase
    {
        private readonly ITorneoService _torneoService;
        public TorneoController(ITorneoService torneoService)
        {
            _torneoService = torneoService;
        }
        [HttpPost]
        public async Task<ActionResult<Torneo>> CreateTorneo (Torneo torneo)
        {
            var response = await _torneoService.CreateTorneo(torneo);
            return CreatedAtAction(nameof(GetTorneo), new { id = torneo.Id }, torneo);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Torneo>> GetTorneo(int id)
        {
            var torneo = await _torneoService.GetTorneo(id);

            if (torneo == null)
            {
                throw new Exception("No se encontro el torneo");
            }

            return torneo;
        }
        [HttpPost("IniciarTorneo/{id}")]
        public async Task<ActionResult<Torneo>> IniciarTorneo(int id)
        {
            var actionResult = await GetTorneo(id);
            if (actionResult.Result is NotFoundResult)
            {
                return NotFound($"El torneo con id '{id}' no existe.");
            }

            var torneo = actionResult.Value;
            if (torneo == null)
            {
                return NotFound($"El torneo con id '{id}' no existe.");
            }
            _torneoService.
            
        }

    }
}
