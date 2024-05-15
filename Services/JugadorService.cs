using Microsoft.EntityFrameworkCore;
using Tennis.API.Models.Request;
using Tennis.Models;
using Tennis.Repository;
using Tennis.Services.Interfaces;

namespace Tennis.Services
{
    public class JugadorService : IJugadorService
    {
        private readonly TennisContext _tennisContext;

        public JugadorService(TennisContext tennisContext)
        {
            _tennisContext = tennisContext;
        }

        public async Task<Jugador> CreateJugador(Jugador jugador)
        {
            Jugador? jugadorExiste = await _tennisContext.Set<Jugador>().Where((e) => e.Dni == jugador.Dni && e.Activo == true).FirstOrDefaultAsync();
            if (jugadorExiste != null)
                throw new Exception($"El jugador con dni '{jugadorExiste.Dni}' ya existe");
            _tennisContext.Add(jugador);
            await _tennisContext.SaveChangesAsync();
            Jugador? response = await _tennisContext.Set<Jugador>().Where((e) => e.Dni == jugador.Dni).FirstOrDefaultAsync();
            return response;
        }
        public async Task<Jugador> EditJugador(Jugador jugador)
        {
            Jugador? jugadorExiste = await _tennisContext.Set<Jugador>().Where((e) => e.Dni == jugador.Dni && e.Activo == true).FirstOrDefaultAsync();
            if (jugadorExiste == null)
                throw new Exception($"El jugador con dni '{jugadorExiste.Dni}' no existe");
            _tennisContext.Update(jugador);
            await _tennisContext.SaveChangesAsync();
            Jugador? response = await _tennisContext.Set<Jugador>().Where((e) => e.Dni == jugador.Dni).FirstOrDefaultAsync();
            return response;
        }
        public async Task<bool> Deleted(int dni)
        {
            Jugador? jugador = await _tennisContext.Set<Jugador>().Where((e) => e.Dni == dni).FirstOrDefaultAsync();
            if (jugador == null)
                throw new Exception($"El jugador con dni '{jugador.Dni}' no existe");
            jugador.Activo = false;
            _tennisContext.Update(jugador);
            int result = await _tennisContext.SaveChangesAsync();
            return result > 0;
        }
    }
}
