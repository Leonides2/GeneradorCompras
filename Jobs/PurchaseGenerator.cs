using GeneradorCompras.Models;
using GeneradorCompras.Models.Service;
using Quartz;
using System.Text.Json;


namespace GeneradorCompras.Jobs
{
    public class PurchaseGenerator : IJob
    {
        private readonly ICompraGenerator compraGenerator;
        private readonly AppDbContext appDbContext;

        public PurchaseGenerator(ICompraGenerator _compraGenerator, AppDbContext _appDbContext)
        {
            compraGenerator = _compraGenerator;
            appDbContext = _appDbContext;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var compra = compraGenerator.GeneratePurchase(1);
            
            
            await appDbContext.SaveChangesAsync();


            Console.WriteLine($"Datos de Compra: {JsonSerializer.Serialize(compra)}");
            
            await Task.CompletedTask;
        }
    }
}
