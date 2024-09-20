using API_Minikids.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Minikids.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaixaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CaixaController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Cadastrar(Caixa caixa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Retorna os erros de validação
            }
            var todasCaixas = await _context.Caixa.ToListAsync();
            caixa.SaldoTotal = Caixa.CalcularSaldoTotal(todasCaixas, caixa);
            _context.Caixa.Add(caixa);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Cadastrar), new { id = caixa.Id }, caixa); // Retorna o recurso criado
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Caixa>>> ListarCaixa()
        {
            var caixa = await _context.Caixa.ToListAsync();
            if(caixa == null || !caixa.Any())
            {
                return NotFound();
            }

            return Ok(caixa);

        }

    }
}
