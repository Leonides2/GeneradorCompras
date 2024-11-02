using Bogus;
using GeneradorCompras.Models.Interface;

namespace GeneradorCompras.Models.Service
{
    public class CompraGenerator: ICompraGenerator
    {
        private readonly IProductService productService;
        private readonly IUserService userService;
        public CompraGenerator(IProductService _productService, IUserService _userService)
        {
            productService = _productService;
            userService = _userService;
        }

        public async Task<List<Compra>> GeneratePurchase(int count)
        {

            var faker = new Faker<Compra>()
                .RuleFor(P => P.ID, f => f.UniqueIndex)
                .RuleFor(P => P.Total, f => 0);

            var compras = faker.Generate(count);

            foreach (var c in compras)
            {
                c.Details = await productService.GetRandomProducts();
                c.User = userService.GetRandomUser();
            }

            foreach (Compra compra in compras)
            {
                compra.Total = compra.Details.Select(product => product.Price).Sum();
            }


            return compras;
        }
    }
}
