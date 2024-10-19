namespace GeneradorCompras.Models.Service
{
    public interface INegocioService
    {
        void GenerateNegocios(int count);

        Task<List<Negocio>> GetNegocios();

        void DeleteNegocios();
    }
}
