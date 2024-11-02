using EventStore.Client;
using GeneradorCompras.Jobs;
using GeneradorCompras.Models;
using GeneradorCompras.Models.Interface;
using GeneradorCompras.Models.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Quartz.Impl;
using System.Text.Json;

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
        public ComprasController(AppDbContext context, EventStoreClient eventStore,
            ICompraGenerator _compraGenerator, INegocioService _negocioService,
            IUserService _userService, ITarjetaService _tarjetaService,
            IProductService _productService

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

        // GET api/<ComprasController>/5
        [HttpGet]
        [Route("ReturnNegocios")]
        public async Task<IActionResult> Get()
        {
            var list = await negocioService.GetNegocios();
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

        // PUT api/<ComprasController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
    }
}
