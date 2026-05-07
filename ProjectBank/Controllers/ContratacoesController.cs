using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectBank.Data;
using ProjectBank.DTOs;
using ProjectBank.Models;
using ProjectBank.Services;

namespace ProjectBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContratacoesController : ControllerBase
    {
        private readonly BancoDbContext _context;
        private readonly IMdrService _mdrService;

        public ContratacoesController(BancoDbContext context, IMdrService mdrService)
        {
            _context = context;
            _mdrService = mdrService;
        }

    
        [HttpPost]
        public async Task<IActionResult> Solicitar([FromBody] ContratacaoRequestDTO dto)
        {
            var cliente = await _context.Clientes
                .Include(c => c.Agencia)
                .FirstOrDefaultAsync(c => c.Id == dto.ClienteId);

            if (cliente is null)
                return NotFound(new { message = $"Cliente {dto.ClienteId} não encontrado." });

            var produto = await _context.Produtos.FindAsync(dto.ProdutoId);
            if (produto is null)
                return NotFound(new { message = $"Produto {dto.ProdutoId} não encontrado." });

            var contratacao = new Contratacao
            {
                ClienteId = cliente.Id,
                ProdutoId = produto.Id,
                DataSolicitacao = DateTime.UtcNow,
                Status = StatusContratacao.Aprovada
            };


            if (produto is MaquinaDeCartao maquina)
            {
                contratacao.TaxaMdrEfetiva = _mdrService.CalcularTaxaEfetiva(maquina, cliente);
                var tipo = cliente is PessoaJuridica ? "PJ" : "PF";
                contratacao.Observacao =
                    $"Taxa MDR aplicada: {contratacao.TaxaMdrEfetiva:F2}% " +
                    $"(cliente {tipo} — taxa base {maquina.TaxaMdrBase:F2}%)";
            }

            _context.Contratacoes.Add(contratacao);
            await _context.SaveChangesAsync();

            await _context.Entry(contratacao).Reference(c => c.Cliente).LoadAsync();
            await _context.Entry(contratacao).Reference(c => c.Produto).LoadAsync();

            return CreatedAtAction(nameof(BuscarPorId), new { id = contratacao.Id },
                MapToResponse(contratacao));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var contratacao = await _context.Contratacoes
                .Include(c => c.Cliente)
                .Include(c => c.Produto)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (contratacao is null)
                return NotFound(new { message = $"Contratação {id} não encontrada." });

            return Ok(MapToResponse(contratacao));
        }

        private static ContratacaoResponseDTO MapToResponse(Contratacao c) => new()
        {
            Id = c.Id,
            ClienteId = c.ClienteId,
            NomeCliente = c.Cliente.Nome,
            ProdutoId = c.ProdutoId,
            NomeProduto = c.Produto.Nome,
            Status = c.Status.ToString(),
            TaxaMdrEfetiva = c.TaxaMdrEfetiva,
            Observacao = c.Observacao,
            DataSolicitacao = c.DataSolicitacao
        };
    }
}
