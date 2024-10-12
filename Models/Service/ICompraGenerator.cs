namespace GeneradorCompras.Models.Service
{
    public interface ICompraGenerator
    {
        public List<Compra> GeneratePurchase(int count);
    }
}
