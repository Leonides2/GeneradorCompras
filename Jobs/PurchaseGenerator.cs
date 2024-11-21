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
using static System.Net.WebRequestMethods;


namespace GeneradorCompras.Jobs
{
    public class PurchaseGenerator : IJob
    {
        private readonly ICompraGenerator compraGenerator;
        private readonly AppDbContext appDbContext;
        private readonly EventStoreClient client;
        private readonly string Ruta;

        public PurchaseGenerator(ICompraGenerator _compraGenerator, AppDbContext _appDbContext, EventStoreClient eventStore)
        {
            compraGenerator = _compraGenerator;
            appDbContext = _appDbContext;
            client = eventStore; 
            Ruta = "https://2m8z6tnp-7120.use.devtunnels.ms/api/LogErrors";
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
                {
                var compra = await compraGenerator.GeneratePurchase(1);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                

                if (compra != null)

                    foreach (var item in compra)
                    {
                        var card = appDbContext.Tarjetas.FirstOrDefault(t => t.ID == item.User.CreditCard);

                        if(card != null && card.State == false)
                        {

                            var ErrorSend = new ErrorDto
                            {
                                purchase = item,
                                isRetriable = true,
                                Code = "",
                                errorType = "Controlado",
                                message = "No se pudo realizar la compra por tarjeta vencida"
                            };


                            Console.WriteLine("enviando error controlado: tarjeta vencida");
                            try
                            {
                                using (var cliente = new HttpClient())
                                {
                                    var JsonResponse = JsonContent.Create(ErrorSend);
                                    using HttpResponseMessage response =
                                    await cliente.PostAsync(Ruta, JsonResponse);

                                    string responseBody = await response.Content.ReadAsStringAsync();
                                   
                                    Console.WriteLine(responseBody);
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

                            var ErrorSend = new ErrorDto
                            {
                                purchase = item,
                                isRetriable = true,
                                Code = "",
                                errorType = "Controlado",
                                message = "No se pudo realizar la compra por fondos insuficientes"
                            };


                            Console.WriteLine("enviando error controlado: fondos insuficientes");
                            try
                            {
                                using (var cliente = new HttpClient())
                                {
                                    var JsonResponse = JsonContent.Create(ErrorSend);
                                    using HttpResponseMessage response =
                                    await cliente.PostAsync(Ruta, JsonResponse);
                                    Console.WriteLine(response.Content);
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
                        }
                    }

                if (compra == null) throw new Exception("Error generando compra");


                compra = null; 
            }
            catch (Exception exception){


                var ErrorSend = new ErrorDto {
                    purchase = null,
                    isRetriable = false,
                    Code = exception.ToString(),
                    errorType = "No controlado",
                    message = exception.Message
                };
                
                using (var cliente = new HttpClient()) {

                   var JsonResponse = JsonContent.Create(ErrorSend);
                   using HttpResponseMessage response = 
                   await cliente.PostAsync(Ruta, JsonResponse);
                    Console.WriteLine(response.Content);
                }
                Console.WriteLine($"Error no controlado");
            }
            await Task.CompletedTask;
        }
    }
}
