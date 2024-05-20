using Tennis.Models;
using Tennis.Models.Request;
using Tennis.Models.Response;

namespace Tennis.Mappers
{
    public static class JugadorMapper
    {
        public static Jugador ToJugador(this JugadorRequest jugadorRequest)
        {
            return new Jugador
            {
                Nombre = jugadorRequest.Nombre,
                Apellido = jugadorRequest.Apellido,
                Nacimiento = jugadorRequest.Nacimiento,
                Genero = jugadorRequest.Genero,
                Habilidad = jugadorRequest.Habilidad,
                Suerte = jugadorRequest.Suerte,
                Fuerza = jugadorRequest.Fuerza,
                Velocidad = jugadorRequest.Velocidad,
                Reaccion = jugadorRequest.Reaccion,
                Activo = jugadorRequest.Activo
            };
        }
        public static JugadorResponse ToJugadorResponse(this Jugador jugadorRequest)
        {
            return new JugadorResponse
            {
                Nombre = jugadorRequest.Nombre,
                Apellido = jugadorRequest.Apellido,
                Nacimiento = jugadorRequest.Nacimiento,
                Genero = jugadorRequest.Genero,
                Habilidad = jugadorRequest.Habilidad,
                Suerte = jugadorRequest.Suerte,
                Fuerza = jugadorRequest.Fuerza,
                Velocidad = jugadorRequest.Velocidad,
                Reaccion = jugadorRequest.Reaccion,
                Activo = jugadorRequest.Activo
            };
        }
    }
}
