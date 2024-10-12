using GeneradorCompras.Models;
using GeneradorCompras.Models.Service;
using Quartz;

namespace GeneradorCompras.Jobs
{
    public class PurchaseGenerator : IJob
    {
        private readonly ICompraGenerator compraGenerator;

        public PurchaseGenerator(ICompraGenerator _compraGenerator)
        {
            compraGenerator = _compraGenerator;
        }

        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine($"Datos de Compra: {compraGenerator.GeneratePurchase(1)}");
            return Task.CompletedTask;
        }
    }
}
