using Bogus;

namespace GeneradorCompras.Models.Service
{
    public class CompraGenerator: ICompraGenerator
    {
        private readonly IProductService productService;
        public CompraGenerator(IProductService _productService)
        {
            productService = _productService;
        }

        public List<Compra> GeneratePurchase(int count)
        {

            var faker = new Faker<Compra>()
                .RuleFor(P => P.ID, f => f.IndexFaker + 1)
                .RuleFor(P => P.CreditCard_N, f => f.Finance.CreditCardNumber())
                .RuleFor(P => P.Details, f => productService.GenerateProduct(f.Random.Int(1, 100)))
                .RuleFor(P => P.Total, f => 0)
                .RuleFor(P => P.IsSuccess, f => f.Random.Bool());

            var compras = faker.Generate(count);

            foreach (Compra compra in compras)
            {
                compra.Total = compra.Details.Select(product => product.Price).Sum();
            }

            return compras;
        }
    }
}
