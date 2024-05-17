using Microsoft.AspNetCore.Mvc;
using Tennis.Models;
using Tennis.Models.Response;

namespace Tennis.Services.Interfaces
{
    public interface ITorneoService
    {
        Task<bool> CreateTorneo(Torneo torneo);
        Task<Torneo> GetTorneo(int id);
        Task<TorneoTerminadoResponse> IniciarTorneo(Torneo torneo);
        Task<List<Torneo>> GetTorneosByFecha(DateTime fechaDesde, DateTime fechaHasta);
    }
}
