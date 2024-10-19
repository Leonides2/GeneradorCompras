using EventStore.Client;
using GeneradorCompras.Jobs;
using GeneradorCompras.Models;
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
        public ComprasController(AppDbContext context, EventStoreClient eventStore, ICompraGenerator _compraGenerator, INegocioService _negocioService) { 
            _context = context;
            client = eventStore;
            compraGenerator = _compraGenerator;
            negocioService = _negocioService;
        }

        [HttpGet("/SubirUnaCompra")]
        public async Task<object> Subscribe()
        {

            var compraList = compraGenerator.GeneratePurchase(1);

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
