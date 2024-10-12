
using Bogus;

namespace GeneradorCompras.Models.Service
{
    public class ProductService : IProductService
    {
        public List<Product> GenerateProduct(int Count)
        {
            var faker = new Faker<Product>()
                .RuleFor(P => P.ID, f => f.IndexFaker + 1)
                .RuleFor(P => P.Price, f => Double.Parse(f.Commerce.Price(1, 1000, 2, "")))
                .RuleFor(P => P.Name, f => f.Name.FullName())
                .RuleFor(P => P.Count, f => f.Random.Int(1, 100));

            return faker.Generate(Count);
        }
    }
}
