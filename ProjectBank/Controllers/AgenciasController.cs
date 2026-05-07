using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectBank.DTOs;
using ProjectBank.Interfaces;
using ProjectBank.Models;

namespace ProjectBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgenciasController : ControllerBase
    {
        private readonly IAgenciaRepository _repository;

        public AgenciasController(IAgenciaRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(AgenciaRequestDTO dto)
        {
            var agencia = new Agencia
            {
                Nome = dto.Nome,
                Numero = dto.Numero
            };

            await _repository.AdicionarAsync(agencia);

            var response = new AgenciaResponseDTO
            {
                Id = agencia.Id,
                Nome = agencia.Nome,
                Numero = agencia.Numero
            };

            return CreatedAtAction(nameof(BuscarPorId), new { id = agencia.Id }, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var agencia = await _repository.ObterPorIdAsync(id);
            if (agencia == null) return NotFound("Agência não encontrada.");

            var response = new AgenciaResponseDTO
            {
                Id = agencia.Id,
                Nome = agencia.Nome,
                Numero = agencia.Numero
            };

            return Ok(response);
        }
    }
}
