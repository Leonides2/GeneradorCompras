using Bogus.DataSets;
using EventStore.Client;
using GeneradorCompras.Jobs;
using GeneradorCompras.Models;
using GeneradorCompras.Models.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Quartz;
using Quartz.Impl;
using System;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GeneradorCompras.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComprasController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly EventStoreClient client;
        private readonly ICompraGenerator compraGenerator;
        private readonly INegocioService negocioService;
        private readonly IProductService productService;
        private readonly ITarjetaService tarjetaService;
        private readonly IUserService userService;
        private readonly IMemoryCache _memory;
        //private readonly ILogger _logger;
        public ComprasController(AppDbContext context, EventStoreClient eventStore,
            ICompraGenerator _compraGenerator, INegocioService _negocioService,
            IUserService _userService, ITarjetaService _tarjetaService,
            IProductService _productService, IMemoryCache memory
            //ILogger logger

            )
        {
            _context = context;
            client = eventStore;
            compraGenerator = _compraGenerator;
            negocioService = _negocioService;
            productService = _productService;
            tarjetaService = _tarjetaService;
            productService = _productService;
            userService = _userService;
            _memory = memory;
            //_logger = logger;
        }

        [HttpGet("/SubirUnaCompra")]
        public async Task<object> Subscribe()
        {

            var compraList = await compraGenerator.GeneratePurchase(1);

            var compra = compraList.Select(u => u);

            foreach (Compra com in compra) {
                var eventData = new EventData(Uuid.NewUuid(),
                               nameof(compra),
                               JsonSerializer.SerializeToUtf8Bytes(com));

                var writeResult = await client.AppendToStreamAsync("test",
                                                      StreamState.Any,
                                                      new[] { eventData });
                return writeResult;
            }

            return "Hello";

        }

        [HttpGet]
        [Route("GenerateResourcer")]
        public string GeenerarRecursos()
        {
            negocioService.GenerateNegocios(100);
            productService.GenerateProduct(100);
            tarjetaService.GenerateTarjetas(50);
            userService.GenerateUsers(30);

            return "RecursosGenerados";
        }

        [HttpGet]
        [Route("DeleteResources")]
        public string EliminarRecursos()
        {
            negocioService.DeleteNegocios();
            productService.DeleteProducts();
            tarjetaService.DeleteTarjetas();
            userService.DeleteUsuarios();

            return "RecursosGenerados";
        }

        // GET api/<ComprasController>/5
        [HttpGet]
        [Route("ReturnNegocios")]
        public async Task<IActionResult> Get()
        {
            var list = await negocioService.GetNegocios();
            return Ok(list);
        }

        [HttpGet]
        [Route("ReturnUsuarios")]
        public async Task<IActionResult> GetU()
        {
            var list = await userService.GetUsarios();
            return Ok(list);
        }

        [HttpGet]
        [Route("ReturnTarjetas")]
        public async Task<IActionResult> GetT()
        {
            var list = await tarjetaService.GetTarjetas();
            return Ok(list);
        }

        [HttpGet]
        [Route("ReturnRandomTarjeta")]
        public async Task<IActionResult> GetRT()
        {
            var list = await tarjetaService.GetRandomCreditCard();
            return Ok(list);
        }

        // POST api/<ComprasController>
        [HttpGet]
        [Route("EliminarNegocios")]
        public string eliminarNegocios()
        {
            negocioService.DeleteNegocios();
            return "Negocios eliminados";
        }


        [HttpPost]
        [Route("Retry")]
        public async Task<bool> Post([FromBody] Compra compra)
        {

            var data = await validateCompra(compra);

            if (data != null)
            {
               return false;
            }

            return true;

        }

        [HttpPost]
        [Route("Success")]
        public async Task PostSucces([FromBody] Compra compra)
        {
            var eventData = new EventData(Uuid.NewUuid(),
                               nameof(compra),
                               JsonSerializer.SerializeToUtf8Bytes(compra));

            try
            {
                var writeResult = await client.AppendToStreamAsync("compras",
                                                  StreamState.Any,
                                                   new[] { eventData });
            }
            catch (Exception ex) { 
                Console.WriteLine(ex.ToString());  
            }

        }

        [HttpPut]
        [Route("tarjeta/{id}")]
        public async Task<Tarjeta> PutNewDatTarjeta(int id, Tarjeta tarjeta)
        {
            var t = await tarjetaService.PutTarjeta(id, tarjeta);

            return t;
        }

        [HttpPost]
        [Route("Method")]
        public async Task<Compra> validateCompra(Compra compra)
        {
            var card = await _context.Tarjetas.FirstOrDefaultAsync(t => t.ID == compra.User.CreditCard);
            if ((card != null && compra.Total < card.Funds) || card != null && card.State == false)
            {
                return compra;
            }
            return null;
        }
    }

    
}
