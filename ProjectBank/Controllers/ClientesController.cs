using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectBank.DTOs;
using ProjectBank.Interfaces;
using ProjectBank.Models;

namespace ProjectBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IAgenciaRepository _agenciaRepository;

        public ClientesController(IClienteRepository clienteRepository, IAgenciaRepository agenciaRepository)
        {
            _clienteRepository = clienteRepository;
            _agenciaRepository = agenciaRepository;
        }

        [HttpPost("pf")]
        public async Task<IActionResult> CadastrarPF(PessoaFisicaRequestDTO dto)
        {
            if (!await _agenciaRepository.ExisteAgenciaAsync(dto.AgenciaId))
                return BadRequest("Agência informada não existe.");

            if (await _clienteRepository.ExisteCpfAsync(dto.Cpf))
                return BadRequest("CPF já cadastrado.");

            var pf = new PessoaFisica
            {
                Nome = dto.Nome,
                AgenciaId = dto.AgenciaId,
                Cpf = dto.Cpf,
                DataNascimento = dto.DataNascimento
            };

            await _clienteRepository.AdicionarPFAsync(pf);
            return CreatedAtAction(nameof(BuscarPorId), new { id = pf.Id }, new { pf.Id, pf.Nome, pf.Cpf, pf.AgenciaId });
        }

        [HttpPost("pj")]
        public async Task<IActionResult> CadastrarPJ(PessoaJuridicaRequestDTO dto)
        {
            if (!await _agenciaRepository.ExisteAgenciaAsync(dto.AgenciaId))
                return BadRequest("Agência informada não existe.");

            if (await _clienteRepository.ExisteCnpjAsync(dto.Cnpj))
                return BadRequest("CNPJ já cadastrado.");

            var pj = new PessoaJuridica
            {
                Nome = dto.Nome,
                AgenciaId = dto.AgenciaId,
                Cnpj = dto.Cnpj,
                RazaoSocial = dto.RazaoSocial
            };

            await _clienteRepository.AdicionarPJAsync(pj);
            return CreatedAtAction(nameof(BuscarPorId), new { id = pj.Id }, new { pj.Id, pj.Nome, pj.Cnpj, pj.AgenciaId });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var cliente = await _clienteRepository.ObterPorIdAsync(id);
            if (cliente == null) return NotFound("Cliente não encontrado.");

            var response = new ClienteResponseDTO
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                AgenciaId = cliente.AgenciaId,
                TipoCliente = cliente is PessoaFisica ? "PF" : "PJ",
                Documento = cliente is PessoaFisica f ? f.Cpf : (cliente as PessoaJuridica)?.Cnpj
            };

            return Ok(response);
        }
    }
}
