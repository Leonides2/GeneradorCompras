// Models/Service/TarjetaService.cs
using Bogus;
using GeneradorCompras.Models;
using GeneradorCompras.Models.Interface;
using GeneradorCompras.Service;
using System.Collections.Generic;

namespace GeneradorCompras.Models.Service
{
    public class TarjetaService : ITarjetaService
    {
        private readonly Faker<Tarjeta> _faker;

        public TarjetaService()
        {
            _faker = new Faker<Tarjeta>()
                .RuleFor(t => t.ID, f => f.Random.Int(1, 1000))
                .RuleFor(t => t.CardNumber, f => f.Finance.CreditCardNumber())
                .RuleFor(t => t.CVV, f => f.Finance.CreditCardCvv())
                .RuleFor(t => t.State, f => f.Random.Bool() ? "Activo" : "Inactivo")
                .RuleFor(t => t.Funds, f => f.Finance.Amount());
        }

        public List<Tarjeta> GenerateTarjetas(int count)
        {
            return _faker.Generate(count);
        }
    }
}
