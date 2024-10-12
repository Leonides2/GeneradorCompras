namespace GeneradorCompras.Models.Service
{
    public interface IProductService
    {
        Task<List<Product>> GenarateProduct(int Count);
    }
}
