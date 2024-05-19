using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tennis.Helpers;
using Tennis.Models;
using Tennis.Models.Entities;
using Tennis.Models.Response;
using Tennis.Repository;
using Tennis.Services.Interfaces;

namespace Tennis.Services
{
    public class TorneoService : ITorneoService
    {
        private readonly TennisContext _tennisContext;
    
        public TorneoService (TennisContext tennisContext)
        {
            _tennisContext = tennisContext;
        }

        public async Task<bool> CreateTorneo(Torneo torneo, int userId)
        {
            if (torneo.TorneoJugador == null || !TorneoValidation.IsPowerOfTwo(torneo.TorneoJugador.Count))
            {
                throw new BadHttpRequestException("La cantidad de jugadores debe ser una potencia de dos.");
            }
            torneo.CreatedByUserId = userId;
            _tennisContext.Set<Torneo>().Add(torneo);
            int resp =  await _tennisContext.SaveChangesAsync() ;

            return resp > 0;
        }
        public async Task<Torneo> GetTorneo(int id)
        {
            return await _tennisContext.Set<Torneo>().Where((e) => e.Id == id)
                                        .Include((e) => e.TorneoJugador).ThenInclude((e) => e.Jugador)
                                        .FirstOrDefaultAsync();
        }
        public async Task<TorneoTerminadoResponse> IniciarTorneo(Torneo torneo)
        {
            TorneoTerminadoResponse resultado = new TorneoTerminadoResponse();
            if (torneo.Genero.ToLower() == "masculino")
            {
                 resultado = TorneoMasculinoIniciar(torneo);
            }
            else
            {
                 resultado =TorneoFemIniciar(torneo);
            }
            return resultado;
        }

        public TorneoTerminadoResponse TorneoMasculinoIniciar(Torneo torneo)
        {
            List<TorneoJugador> torneoJugador = torneo.TorneoJugador;
            while (torneoJugador.Count > 1)
            {
                var ganadoresRonda = new List<TorneoJugador>();

                for (int i = 0; i < torneoJugador.Count; i += 2)
                {
                    var jugador1 = torneoJugador[i];
                    var jugador2 = torneoJugador[i + 1];

                    var ganadorEnfrentamiento = SimularEnfrentamientoMasculino(jugador1, jugador2);
                    var partido = new Partido();
                    partido.IdTorneo = torneo.Id;
                    partido.IdJugadorL = ganadorEnfrentamiento.IdJugador == jugador1.IdJugador ? jugador2.IdJugador : jugador1.IdJugador;
                    partido.IdJugadorW = ganadorEnfrentamiento.IdJugador;
                    _tennisContext.Set<Partido>().Add(partido);
                    _tennisContext.SaveChanges();
                    ganadoresRonda.Add(ganadorEnfrentamiento);
                }
                torneoJugador = ganadoresRonda;
            }

            var ganador = new TorneoTerminadoResponse();
            ganador.IdJugador = torneoJugador[0].IdJugador;
            ganador.JugadorGanador = torneoJugador[0].Jugador;
            return ganador;
        }
        public TorneoTerminadoResponse TorneoFemIniciar(Torneo torneo)
        {
            var res = new TorneoTerminadoResponse();
            return res; 
        }

        public TorneoJugador SimularEnfrentamientoMasculino (TorneoJugador jug1, TorneoJugador jug2)
        {
            int puntaje1 = CalcularPuntajeMasculino(jug1.Jugador);
            int puntaje2 = CalcularPuntajeMasculino(jug2.Jugador);
            if(jug1.Jugador.Suerte > jug2.Jugador.Suerte)
            {
                puntaje1 += 1;
            }
            else
            {
                puntaje2 += 1;
            }
            return puntaje1 > puntaje2 ? jug1 : jug2;
        }

        private int CalcularPuntajeMasculino(Jugador jugador)
        {
            int puntaje = jugador.Habilidad + jugador.Fuerza + jugador.Velocidad;
            return puntaje;
        }
        public TorneoJugador SimularEnfrentamientoFem(TorneoJugador jug1, TorneoJugador jug2)
        {
            int puntaje1 = CalcularPuntajeFem(jug1.Jugador);
            int puntaje2 = CalcularPuntajeFem(jug2.Jugador);
            if (jug1.Jugador.Suerte > jug2.Jugador.Suerte)
            {
                puntaje1 += 1;
            }
            else
            {
                puntaje2 += 1;
            }
            return puntaje1 > puntaje2 ? jug1 : jug2;
        }
        private int CalcularPuntajeFem(Jugador jugador)
        {
            int puntaje = jugador.Habilidad + jugador.Reaccion;
            return puntaje;
        }

        public async Task<List<Torneo>> GetTorneosByFecha(DateTime fechaDesde, DateTime fechaHasta)
        {
            List<Torneo> torneos = await _tennisContext.Set<Torneo>()
                .Where(t => t.FechaTermino >= fechaDesde && t.FechaTermino <= fechaHasta)
                .Include(t => t.TorneoJugador).ThenInclude(e => e.Jugador)
                .Include(e => e.JugadorW)
                .Include(e => e.Partido)
                .AsNoTracking()
                .ToListAsync();
            return torneos;
        }

    }
}
