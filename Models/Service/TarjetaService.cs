// Models/Service/TarjetaService.cs
using Bogus;
using GeneradorCompras.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GeneradorCompras.Models.Service
{
    public class TarjetaService : ITarjetaService
    {

        private readonly AppDbContext _appDbContext;

        public TarjetaService(AppDbContext context)
        {
            _appDbContext = context;
        }

        public async void DeleteTarjetas()
        {
            var list = await _appDbContext.Tarjetas.ToListAsync();

            _appDbContext.Tarjetas.RemoveRange(list);
            await _appDbContext.SaveChangesAsync();
        }

        public async void GenerateTarjetas(int count)
        {

            var _faker = new Faker<Tarjeta>()
                .RuleFor(t => t.CardNumber, f => f.Finance.CreditCardNumber())
                .RuleFor(t => t.CVV, f => f.Finance.CreditCardCvv())
                .RuleFor(t => t.State, f => f.Random.Bool())
                .RuleFor(t => t.Funds, f => (Double) f.Finance.Amount(0,100000));

            _appDbContext.Tarjetas.AddRange(_faker.Generate(count));
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<Tarjeta> GetRandomCreditCard()
        {
            var random = new Random();
            var list = await _appDbContext.Tarjetas.ToListAsync();
            var maxIndex = list.Count();
            var randomCard = random.Next(maxIndex);

            return list[randomCard];
        }

        public Task<List<Tarjeta>> GetTarjetas()
        {
            return _appDbContext.Tarjetas.ToListAsync();
        }

        public async Task<Tarjeta> PutTarjeta(int id, Tarjeta tarjeta)
        {
            var tarjeta1 =  await _appDbContext.Tarjetas.FirstOrDefaultAsync( t => t.ID == id);

            if (tarjeta1 != null)
            {
                tarjeta1.State = tarjeta.State;
                tarjeta1.Funds = tarjeta.Funds;
                tarjeta1.CardNumber = tarjeta.CardNumber;
                tarjeta1.CVV = tarjeta.CVV;
            }

            await _appDbContext.SaveChangesAsync();


            return tarjeta1;
        }
    }
}
