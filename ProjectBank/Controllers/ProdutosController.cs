using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectBank.Data;
using ProjectBank.DTOs;
using ProjectBank.Models;

namespace ProjectBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly BancoDbContext _context;

        public ProdutosController(BancoDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] MaquinaDeCartaoRequestDTO dto)
        {
            var produto = new MaquinaDeCartao
            {
                Nome = dto.Nome,
                Descricao = dto.Descricao,
                ModeloEquipamento = dto.ModeloEquipamento,
                TaxaMdrBase = dto.TaxaMdrBase,
                Ativo = 1
            };

            _context.MaquinasDeCartao.Add(produto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(BuscarPorId), new { id = produto.Id },
                new ProdutoResponseDTO
                {
                    Id = produto.Id,
                    Nome = produto.Nome,
                    Descricao = produto.Descricao,
                    ModeloEquipamento = produto.ModeloEquipamento,
                    TaxaMdrBase = produto.TaxaMdrBase
                });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var produto = await _context.MaquinasDeCartao.FindAsync(id);
            if (produto is null)
                return NotFound(new { message = $"Produto {id} não encontrado." });

            return Ok(new ProdutoResponseDTO
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                ModeloEquipamento = produto.ModeloEquipamento,
                TaxaMdrBase = produto.TaxaMdrBase
            });
        }
    }
}
