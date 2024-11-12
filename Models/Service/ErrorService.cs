
using Bogus;

namespace GeneradorCompras.Models.Service
{
    public class ErrorService : IErrorService
    {
        public Error GenerateError()
        {
            var fake = new Faker<Error>().
                RuleFor(e => e.Id, f => f.UniqueIndex);

            var error  = fake.Generate(1);

            var err = error[0];
            return err;
        }
    }
}
