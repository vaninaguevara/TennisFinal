using Tennis.Models;

namespace Tennis.Services.Interfaces
{
    public interface IJugadorService
    {
        Task<Jugador> CreateJugador(Jugador jugador);
        Task<Jugador> EditJugador(Jugador jugador);
        Task<bool> Deleted(int dni);
    }
}
