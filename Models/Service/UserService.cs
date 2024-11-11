// Models/Service/UserService.cs
using Bogus;
using GeneradorCompras.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GeneradorCompras.Models.Service
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ITarjetaService _tarjetaService;
        public UserService( AppDbContext context, ITarjetaService tarjetaService)
        {
            _appDbContext = context;
            _tarjetaService = tarjetaService;
        }

        public async void DeleteUsuarios()
        {
            var userList = _appDbContext.Usuarios.ToList();

            _appDbContext.Usuarios.RemoveRange(userList);
            await _appDbContext.SaveChangesAsync(); 
        }

        public async void GenerateUsers(int count)
        {
            var _faker = new Faker<User>()
                .RuleFor(u => u.Name, f => f.Name.FullName())
                .RuleFor(u => u.Provincia, f => f.Address.State())
                .RuleFor(u => u.Canton, f => f.Random.Int(1, 100));

            var usuarios = _faker.Generate(count);

            foreach (var u in usuarios)
            {
                u.CreditCard = (await _tarjetaService.GetRandomCreditCard()).ID;
            }

            _appDbContext.Usuarios.AddRange(usuarios);
            await _appDbContext.SaveChangesAsync();
        }

        public User GetRandomUser()
        {
            var random = new Random();
            var userList = _appDbContext.Usuarios.ToList();
            var maxIndex = userList.Count();
            var randomUser = random.Next(maxIndex);
            

            return userList[randomUser];
        }

        public Task<List<User>> GetUsarios()
        {
            return _appDbContext.Usuarios.ToListAsync();
        }
    }
}
