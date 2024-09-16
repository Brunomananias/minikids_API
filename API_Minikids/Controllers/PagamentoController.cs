using API_Minikids.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Minikids.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagamentoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PagamentoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Pagamento>> CreatePagamento(Pagamento pagamento)
        {
            _context.Pagamento.Add(pagamento);
            await _context.SaveChangesAsync();

            // Certifique-se de que `GetPagamentosPorEvento` é o nome correto da ação
            return CreatedAtAction(nameof(GetPagamentosPorEvento), new { eventoId = pagamento.EventoId }, pagamento);
        }

        [HttpGet("{eventoId}")]
        public async Task<ActionResult<IEnumerable<Pagamento>>> GetPagamentosPorEvento(int eventoId)
        {
            var pagamentos = await _context.Pagamento
                .Where(p => p.EventoId == eventoId)
                .ToListAsync();

            return pagamentos;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePagamento(int id)
        {
            // Verifica se o pagamento existe
            var pagamento = await _context.Pagamento.FindAsync(id);

            if (pagamento == null)
            {
                return NotFound(); // Retorna 404 se o pagamento não for encontrado
            }

            _context.Pagamento.Remove(pagamento); // Remove o pagamento do contexto
            await _context.SaveChangesAsync(); // Salva as alterações no banco de dados

            return NoContent(); // Retorna 204 No Content para indicar sucesso
        }
    }
}
