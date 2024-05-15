using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tennis.Models;
using Tennis.Services.Interfaces;

namespace Tennis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JugadorController : ControllerBase
    {
        private readonly IJugadorService _jugadorService;
        public JugadorController(IJugadorService jugadorService)
        {
            _jugadorService = jugadorService;
        }
        [HttpPost]
        public async Task<Jugador> CreateJugador (Jugador jugador)
        {
            Jugador response = await _jugadorService.CreateJugador(jugador);
            return response;
        }
        [HttpPut]
        public async Task<Jugador> EditJugador (Jugador jugador)
        {
            Jugador response = await _jugadorService.EditJugador(jugador);
            return response;
        }
        [HttpPost, Route("Deleted/{dni}")]
        public async Task<bool> DeletedJugador(int dni)
        {
            return await _jugadorService.Deleted(dni);
        }

    }
}
