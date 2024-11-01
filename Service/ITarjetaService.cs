// Models/Interface/ITarjetaService.cs
using GeneradorCompras.Models;
using System.Collections.Generic;

namespace GeneradorCompras.Models.Interface
{
    public interface ITarjetaService
    {
        List<Tarjeta> GenerateTarjetas(int count);
    }
}
