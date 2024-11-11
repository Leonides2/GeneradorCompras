// Models/Interface/IUserService.cs
using System.Collections.Generic;

namespace GeneradorCompras.Models.Service
{
    public interface IUserService
    {
        void GenerateUsers(int count);

        Task<List<User>> GetUsarios();

        void DeleteUsuarios();

        User GetRandomUser();
    }
}
