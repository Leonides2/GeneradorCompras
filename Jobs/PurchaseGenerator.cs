using GeneradorCompras.Models;
using GeneradorCompras.Models.Service;
using Quartz;
using System.Text.Json;


namespace GeneradorCompras.Jobs
{
    public class PurchaseGenerator : IJob
    {
        private readonly ICompraGenerator compraGenerator;

        public PurchaseGenerator(ICompraGenerator _compraGenerator)
        {
            compraGenerator = _compraGenerator;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var compra = compraGenerator.GeneratePurchase(1);

            Console.WriteLine($"Datos de Compra: {JsonSerializer.Serialize(compra)}");
            
            await Task.CompletedTask;
        }
    }
}
