using API_Minikids.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Minikids.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EventosController(AppDbContext context)
        {
            _context = context;
        }

        // GET api/eventos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Evento>>> GetEventos()
        {
            return await _context.Eventos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventoById(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);

            if (evento == null)
            {
                return NotFound();
            }

            return Ok(evento);
        }


        [HttpPost]
        public async Task<IActionResult> PostEvento([FromBody] Evento eventoDto)
        {
        // Verificar se o cliente existe
        var cliente = await _context.Clientes.FindAsync(eventoDto.ClienteId);
            if (cliente == null)
            {
                return NotFound("Cliente não encontrado.");
            }

            // Criar um novo evento
            var evento = new Evento
            {
                Data = eventoDto.Data,
                Pacote = eventoDto.Pacote,
                TempoDeFesta = eventoDto.TempoDeFesta, // Certifique-se de que está no formato correto
                Endereco = eventoDto.Endereco,
                Observacoes = eventoDto.Observacoes,
                ValorTotalPacote = eventoDto.ValorTotalPacote,
                ClienteId = eventoDto.ClienteId
            };

            // Adicionar o evento ao contexto
            _context.Eventos.Add(evento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEventoById), new { id = evento.Id }, evento);
        }


        // PUT api/eventos/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvento(int id, Evento evento)
        {
            if (id != evento.Id)
            {
                return BadRequest();
            }

            _context.Entry(evento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE api/eventos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvento(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null)
            {
                return NotFound();
            }

            _context.Eventos.Remove(evento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventoExists(int id)
        {
            return _context.Eventos.Any(e => e.Id == id);
        }
    }

}
