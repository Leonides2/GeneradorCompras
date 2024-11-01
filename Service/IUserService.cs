// Models/Interface/IUserService.cs
using GeneradorCompras.Models;
using System.Collections.Generic;

namespace GeneradorCompras.Models.Interface
{
    public interface IUserService
    {
        List<User> GenerateUsers(int count);
    }
}
