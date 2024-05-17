using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tennis.Models;
using Tennis.Models.Response;
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
            if(torneo.GetType() == typeof(Torneo))
            {
                var response = await _torneoService.CreateTorneo(torneo);
                return CreatedAtAction(nameof(GetTorneo), new { id = torneo.Id }, torneo);
            }
            else
            {
                throw new Exception("Solo se admite datos para crear el torneo");

            }
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
        public async Task<ActionResult<TorneoTerminadoResponse>> IniciarTorneo(int id)
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
            TorneoTerminadoResponse response = await _torneoService.IniciarTorneo(torneo);
            return response;
            
        }
        [HttpGet("getTorneos/{fechaDesde}/{fechaHasta}")]
        public async Task<ActionResult<List<Torneo>>> GetTorneosByFecha(DateTime fechaDesde, DateTime fechaHasta)

        {
            try
            {
                if (fechaHasta > DateTime.Today)
                {
                    return BadRequest("La fecha hasta no puede ser mayor que la fecha actual.");
                }

                var torneos = await _torneoService.GetTorneosByFecha(fechaDesde, fechaHasta);
                return Ok(torneos);
            }catch (Exception ex)
            {
                if(ex.Message == "Type of object ")
                {
                    return BadRequest("La fecha tiene que se del tipo 'yyyy/mm/dd'");
                }
                return BadRequest(ex.Message);
            }
        }
    }
}
