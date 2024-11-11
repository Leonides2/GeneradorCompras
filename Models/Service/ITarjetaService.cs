// Models/Interface/ITarjetaService.cs
using System.Collections.Generic;

namespace GeneradorCompras.Models.Service
{
    public interface ITarjetaService
    {
        void GenerateTarjetas(int count);
        Task<List<Tarjeta>> GetTarjetas();

        Task<Tarjeta> PutTarjeta(int id, Tarjeta tarjeta);

        void DeleteTarjetas();

        Task<Tarjeta> GetRandomCreditCard();
    }
}
