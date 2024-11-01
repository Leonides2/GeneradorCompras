// Models/Service/UserService.cs
using Bogus;
using GeneradorCompras.Models;
using GeneradorCompras.Models.Interface;
using System.Collections.Generic;

namespace GeneradorCompras.Models.Service
{
    public class UserService : IUserService
    {
        private readonly Faker<User> _faker;

        public UserService()
        {
            _faker = new Faker<User>()
                .RuleFor(u => u.ID, f => f.Random.Int(1, 1000))
                .RuleFor(u => u.Name, f => f.Name.FullName())
                .RuleFor(u => u.Provincia, f => f.Address.State())
                .RuleFor(u => u.Canton, f => f.Random.Int(1, 100));
        }

        public List<User> GenerateUsers(int count)
        {
            return _faker.Generate(count);
        }
    }
}
