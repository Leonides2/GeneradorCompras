using GeneradorCompras.Models;
using Quartz;

namespace GeneradorCompras.Jobs
{
    public class PurchaseGenerator : IJob
    {
        public Compra GenerateRandomPurchase()
        {
            return new Compra
            {
                ID = 1,
                ProductName = "Lavadora",
                Count = 1,
                CreditCard_N = 100000000,
                IsSuccess = true
            };
        }

        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine($"Datos de Compra: {GenerateRandomPurchase()}");
            return Task.CompletedTask;
        }
    }
}
