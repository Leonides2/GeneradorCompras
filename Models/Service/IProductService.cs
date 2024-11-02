namespace GeneradorCompras.Models.Service
{
    public interface IProductService
    {
        void GenerateProduct(int Count);
        Task<List<Product>> GetProducts();

        void DeleteProducts();

        Task<List<Product>> GetRandomProducts();
    }
}
