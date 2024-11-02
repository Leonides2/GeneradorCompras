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
            var compra = await compraGenerator.GeneratePurchase(1);
            await appDbContext.SaveChangesAsync();

            if(compra != null)
            {
                foreach (var item in compra)
                {
                    var card =  appDbContext.Tarjetas.FirstOrDefault(t => t.ID == item.User.CreditCard);
                    if (card != null && item.Total < card.Funds)
                    {
                        var purchaseError = new Error
                        {
                            Time = DateTime.Now,
                            Type = "Controlado",
                            Description = "No hay suficientes fondos.",
                            Code = null,
                            Message = ""
                        };
                        Console.WriteLine($"No se pudo generar la compra exitosamente: {JsonSerializer.Serialize(item)}");
                        Console.WriteLine($"Error: {JsonSerializer.Serialize(purchaseError)}");
                    }
                }
            }
            else
            {
                
            }


            Console.WriteLine($"Datos de Compra: {JsonSerializer.Serialize(compra)}");
            
            await Task.CompletedTask;
        }
    }
}
