// Models/Interface/ITarjetaService.cs
using GeneradorCompras.Models;
using System.Collections.Generic;

namespace GeneradorCompras.Models.Interface
{
    public interface ITarjetaService
    {
        void GenerateTarjetas(int count);
        Task<List<Tarjeta>> GetTarjetas();

        void DeleteTarjetas();

        Task<Tarjeta> GetRandomCreditCard();
    }
}
