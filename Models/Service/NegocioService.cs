﻿
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace GeneradorCompras.Models.Service
{
    public class NegocioService : INegocioService
    {
        private readonly AppDbContext _appDbContext;
        public NegocioService( AppDbContext appDbContext) 
        {
            _appDbContext = appDbContext;
        }

        public async void GenerateNegocios(int count)
        {
            var faker = new Faker<Negocio>().
                RuleFor(p => p.Name, f => f.Company.CompanyName());

            _appDbContext.Negocios.AddRange(faker.Generate(count));
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<Negocio>> GetNegocios()
        {
            var negocios = await _appDbContext.Negocios.ToListAsync();

            return negocios;
        }

        public async void DeleteNegocios()
        {
            var list = await _appDbContext.Negocios.ToListAsync();

            _appDbContext.Negocios.RemoveRange(list);
            await _appDbContext.SaveChangesAsync();
        }

        public Negocio GetRandomNegocio()
        {
            var random = new Random();
            var userList = _appDbContext.Negocios.ToList();
            var maxIndex = userList.Count();
            var randomUser = random.Next(maxIndex);


            return userList[randomUser];
        }
    }
}
