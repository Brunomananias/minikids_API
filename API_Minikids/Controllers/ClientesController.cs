using API_Minikids.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Minikids.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientesController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("{clienteId}/pagamentos")]
        public async Task<ActionResult<IEnumerable<object>>> GetPagamentos(int clienteId)
        {
            var eventos = await _context.Eventos
                .Where(e => e.ClienteId == clienteId)
                .Include(e => e.Pagamentos)
                .ToListAsync();

            if (eventos == null || !eventos.Any())
            {
                return NotFound(); // Retorna 404 se nenhum evento for encontrado
            }

            var resultados = eventos.Select(e => new
            {
                EventoId = e.Id,
                ValorTotalPacote = e.ValorTotalPacote,
                ValorPago = e.Pagamentos.Sum(p => p.ValorPago),
                ValorRestante = e.ValorTotalPacote - e.Pagamentos.Sum(p => p.ValorPago)
            });

            return Ok(resultados);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetClientesWithEventos()
        {
            var clientes = await _context.Clientes
                .Include(c => c.Eventos) // Inclui os eventos relacionados
                .ToListAsync();

            return Ok(clientes);
        }

        // GET: api/Clientes/{id}
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            // Inclui os eventos associados ao cliente
            var cliente = await _context.Clientes
                .Include(c => c.Eventos)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCliente), new { id = cliente.Id }, cliente);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return BadRequest();
            }

            _context.Entry(cliente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            // Encontre o cliente com eventos e pagamentos incluídos
            var cliente = await _context.Clientes
                .Include(c => c.Eventos)
                .ThenInclude(e => e.Pagamentos)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cliente == null)
            {
                return NotFound();
            }

            // Remove todos os pagamentos associados aos eventos do cliente
            foreach (var evento in cliente.Eventos)
            {
                _context.Pagamento.RemoveRange(evento.Pagamentos);
            }

            // Remove todos os eventos associados ao cliente
            _context.Eventos.RemoveRange(cliente.Eventos);

            // Remove o cliente
            _context.Clientes.Remove(cliente);

            try
            {
                await _context.SaveChangesAsync();
                return NoContent(); // Ou outro código de status apropriado
            }
            catch (DbUpdateException ex)
            {
                // Log detalhado do erro
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno do servidor");
            }
        }

        [Authorize]
        [HttpGet("{id}/eventos")]
        public async Task<ActionResult<IEnumerable<Evento>>> GetEventosByCliente(int id)
        {
            var cliente = await _context.Clientes.Include(c => c.Eventos).SingleOrDefaultAsync(c => c.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente.Eventos);
        }
    }
}
