// Models/Interface/IUserService.cs
using GeneradorCompras.Models;
using System.Collections.Generic;

namespace GeneradorCompras.Models.Interface
{
    public interface IUserService
    {
        void GenerateUsers(int count);

        Task<List<User>> GetUsarios();

        void DeleteUsuarios();

        User GetRandomUser();
    }
}
