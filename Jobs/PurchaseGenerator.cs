using EventStore.Client;
using GeneradorCompras.Models;
using GeneradorCompras.Models.Service;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Quartz;
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text.Json;
using System.Net.Sockets;


namespace GeneradorCompras.Jobs
{
    public class PurchaseGenerator : IJob
    {
        private readonly ICompraGenerator compraGenerator;
        private readonly AppDbContext appDbContext;
        private readonly EventStoreClient client;
        private readonly IErrorService errorService;
        private readonly string localIp;

        public PurchaseGenerator(ICompraGenerator _compraGenerator, AppDbContext _appDbContext, EventStoreClient eventStore, IErrorService error)
        {
            compraGenerator = _compraGenerator;
            appDbContext = _appDbContext;
            client = eventStore; 
            errorService = error;
            localIp = "26.245.229.75";
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
                {
                var compra = await compraGenerator.GeneratePurchase(1);

                    if(compra != null)

                    foreach (var item in compra)
                    {
                        var card = appDbContext.Tarjetas.FirstOrDefault(t => t.ID == item.User.CreditCard);
                        var error = errorService.GenerateError();

                        if(card != null && card.State == false)
                        {
                            error.ErrorObject = item;

                            var ErrorSend = new ErrorDto
                            {
                                error = error,
                                IsRetriable = true,
                                Code = null,
                                errorType = "Controlado",
                                Message = "No se pudo realizar la compra por tarjeta vencida"
                            };


                            Console.WriteLine("enviando error controlado: tarjeta vencida");
                            try
                            {
                                using (var cliente = new HttpClient())
                                {
                                    var JsonResponse = JsonContent.Create(ErrorSend);
                                    using HttpResponseMessage response =
                                    await cliente.PostAsync("http://26.46.46.157:5178/api/LogErrors", JsonResponse);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                            }

                            Console.WriteLine($"No se pudo generar la compra exitosamente: {item.ID}");

                    }
                        if (card != null && item.Total > card.Funds)
                        {

                            error.ErrorObject = item;

                            var ErrorSend = new ErrorDto
                            {
                                error = error,
                                IsRetriable = true,
                                Code = null,
                                errorType = "Controlado",
                                Message = "No se pudo realizar la compra por fondos insuficientes"
                            };


                            Console.WriteLine("enviando error controlado: fondos insuficientes");
                            try
                            {
                                using (var cliente = new HttpClient())
                                {
                                    var JsonResponse = JsonContent.Create(ErrorSend);
                                    using HttpResponseMessage response =
                                    await cliente.PostAsync("http://26.46.46.157:5178/api/LogErrors", JsonResponse);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                            }

                            Console.WriteLine($"No se pudo generar la compra exitosamente: {item.ID}");


                        }

                        if (card != null && item.Total < card.Funds)
                        {

                            item.IsSuccess = true;

                            var compraUnic = compra.Select(u => u);

                            foreach (Compra com in compra)
                            {
                                var eventData = new EventData(Uuid.NewUuid(),
                                               nameof(compra),
                                               JsonSerializer.SerializeToUtf8Bytes(com));

                                var writeResult = await client.AppendToStreamAsync("Compra",
                                                                      StreamState.Any,
                                                                      new[] { eventData });
                            }

                            Console.WriteLine($"Compra realizada exitosamente: {JsonSerializer.Serialize(item)}");
                            Console.WriteLine("");
                        }
                    }

                if (compra == null) throw new Exception("Error generando compra");


                compra = null; 
            }
            catch (Exception exception){

                var error = errorService.GenerateError();

                var ErrorSend = new ErrorDto {
                    error = error,
                    IsRetriable = false,
                    Code = exception.ToString(),
                    errorType = "No controlado",
                    Message = exception.Message
                };
                
                using (var cliente = new HttpClient()) {

                   var JsonResponse = JsonContent.Create(error);
                   using HttpResponseMessage response = 
                   await cliente.PostAsync("http://26.46.46.157:5178/api/LogErrors", JsonResponse);
                }
                Console.WriteLine($"Error: {JsonSerializer.Serialize(ErrorSend)}");
            }
            await Task.CompletedTask;
        }
    }
}
