using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ProjectBank.Data;
using ProjectBank.Models;
using ProjectBank.Repositories;
using ProjectBank.Services;
using ProjectBank.Controllers;
using ProjectBank.DTOs;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace ProjectBank.Tests
{
    public class BancoTests
    {
        private BancoDbContext CriarContexto(string nome)
        {
            var options = new DbContextOptionsBuilder<BancoDbContext>()
                .UseInMemoryDatabase(nome)
                .Options;
            return new BancoDbContext(options);
        }

        
        [Fact]
        public async Task ExisteCpf_DeveRetornarTrue_QuandoCpfJaCadastrado()
        {
            using var db = CriarContexto("cpf_duplicado");
            db.PessoasFisicas.Add(new PessoaFisica
            {
                Nome = "João",
                Cpf = "111.111.111-11",
                DataNascimento = DateTime.Now,
                AgenciaId = 1
            });
            await db.SaveChangesAsync();

            var repo = new ClienteRepository(db);
            var existe = await repo.ExisteCpfAsync("111.111.111-11");

            existe.Should().BeTrue();
        }

        [Fact]
        public async Task ExisteCnpj_DeveRetornarTrue_QuandoCnpjJaCadastrado()
        {
            using var db = CriarContexto("cnpj_duplicado");
            db.PessoasJuridicas.Add(new PessoaJuridica
            {
                Nome = "Empresa",
                Cnpj = "11.222.333/0001-44",
                RazaoSocial = "Empresa SA",
                AgenciaId = 1
            });
            await db.SaveChangesAsync();

            var repo = new ClienteRepository(db);
            var existe = await repo.ExisteCnpjAsync("11.222.333/0001-44");

            existe.Should().BeTrue();
        }

        [Fact]
        public async Task ExisteAgencia_DeveRetornarFalse_QuandoAgenciaNaoExiste()
        {
            using var db = CriarContexto("agencia_inexistente");
            var repo = new AgenciaRepository(db);

            var existe = await repo.ExisteAgenciaAsync(999);

            existe.Should().BeFalse();
        }

        [Fact]
        public void MDR_PF_DevePagarTaxaCheia()
        {
            var maquina = new MaquinaDeCartao
            {
                Id = 1,
                Nome = "POS",
                Descricao = "",
                Ativo = 1,
                ModeloEquipamento = "POS",
                TaxaMdrBase = 1.99m
            };
            var pf = new PessoaFisica
            {
                Nome = "João",
                Cpf = "123",
                AgenciaId = 1,
                DataNascimento = DateTime.Now
            };

            var service = new MdrService();
            var taxa = service.CalcularTaxaEfetiva(maquina, pf);

            taxa.Should().Be(1.99m);
        }

        [Fact]
        public async Task Contratacao_DeveRetornar404_QuandoClienteInexistente()
        {
            using var db = CriarContexto("contratacao_cliente_inexistente");
            var controller = new ContratacoesController(db, new MdrService());

            var result = await controller.Solicitar(new ContratacaoRequestDTO
            {
                ClienteId = 999,
                ProdutoId = 1
            });

            result.Should().BeOfType<NotFoundObjectResult>();
        }

     
        [Fact]
        public void MDR_PJ_DeveReceberDesconto30Porcento()
        {
            var maquina = new MaquinaDeCartao
            {
                Id = 1,
                Nome = "POS",
                Descricao = "",
                Ativo = 1,
                ModeloEquipamento = "POS",
                TaxaMdrBase = 2.00m
            };
            var pj = new PessoaJuridica
            {
                Nome = "Empresa",
                Cnpj = "123",
                RazaoSocial = "SA",
                AgenciaId = 1
            };

            var service = new MdrService();
            var taxa = service.CalcularTaxaEfetiva(maquina, pj);

            taxa.Should().Be(1.40m);
        }
    }
}