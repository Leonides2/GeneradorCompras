namespace GeneradorCompras.Models.Service
{
    public interface ICompraGenerator
    {
        Task<List<Compra>> GeneratePurchase(int count);
    }
}
