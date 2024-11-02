
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace GeneradorCompras.Models.Service
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _appDbContext;
        private readonly INegocioService _nggocioService;

        public ProductService(AppDbContext appDbContext, INegocioService negocioService)
        {
            _appDbContext = appDbContext;
            _nggocioService = negocioService;
        }

        public async void DeleteProducts()
        {
            var productsList = _appDbContext.Productos.ToList();

            _appDbContext.Productos.RemoveRange(productsList);
            await _appDbContext.SaveChangesAsync();
        }

        public async void GenerateProduct(int Count)
        {
            var faker = new Faker<Product>()
                .RuleFor(P => P.Price, f => Double.Parse(f.Commerce.Price(1, 1000, 2, "")))
                .RuleFor(P => P.Name, f => f.Name.FullName())
                .RuleFor(P => P.Count, f => f.Random.Int(1, 100))
                .RuleFor(P => P.Category, f => f.Commerce.Department());

            var products = faker.Generate(Count);
            
            foreach (var p in products)
            {
                p.CommerceID = _nggocioService.GetRandomNegocio().ID;
            }

            _appDbContext.Productos.AddRange(products);
            await _appDbContext.SaveChangesAsync();
        }

        public Task<List<Product>> GetProducts()
        {
            return _appDbContext.Productos.ToListAsync();
        }

        public async Task<List<Product>> GetRandomProducts()
        {
            var random = new Random();
            var productsList = _appDbContext.Productos.ToList();
            var maxIndex = productsList.Count();
            var index = random.Next(maxIndex);
            List<Product> products = new();

            for (int i = 0; i < index; i++)
            {
                var randomProduct = random.Next(maxIndex);
                var product = productsList[randomProduct];
                if(product != null)
                {
                    products.Add(product);
                }
            }

            return products;
        }
    }
}
