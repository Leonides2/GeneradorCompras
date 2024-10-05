using GeneradorCompras.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GeneradorCompras.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComprasController : ControllerBase
    {   
        private readonly AppDbContext _context;
        public ComprasController(AppDbContext context) { 
            _context = context;
        }
        [HttpGet]
        public async Task<List<Compra>> Get()
        {
            return await _context.Compras.ToListAsync();
        }

        // GET api/<ComprasController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ComprasController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ComprasController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
    }
}
