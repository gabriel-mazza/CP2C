using Microsoft.EntityFrameworkCore;
using ProjectBank.Models;

namespace ProjectBank.Data
{
    public class BancoDbContext : DbContext
    {
        public BancoDbContext(DbContextOptions<BancoDbContext> options) : base(options) { }

        // Dev 1
        public DbSet<Agencia> Agencias { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<PessoaFisica> PessoasFisicas { get; set; }
        public DbSet<PessoaJuridica> PessoasJuridicas { get; set; }

        // Dev 2
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<MaquinaDeCartao> MaquinasDeCartao { get; set; }
        public DbSet<Contratacao> Contratacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ── Herança Cliente (código do Dev 1 — não alterado) ──
            modelBuilder.Entity<Cliente>()
                .HasDiscriminator<string>("TipoCliente")
                .HasValue<PessoaFisica>("PF")
                .HasValue<PessoaJuridica>("PJ");

            modelBuilder.Entity<PessoaFisica>()
                .HasIndex(p => p.Cpf).IsUnique();

            modelBuilder.Entity<PessoaJuridica>()
                .HasIndex(p => p.Cnpj).IsUnique();

            // ── Herança Produto (Dev 2) ────────────────────────────
            modelBuilder.Entity<Produto>()
                .HasDiscriminator<string>("TipoProduto")
                .HasValue<MaquinaDeCartao>("MAQUINA_CARTAO");

            // Oracle: NUMBER(10,2) para decimais
            modelBuilder.Entity<MaquinaDeCartao>()
                .Property(p => p.TaxaMdrBase)
                .HasColumnType("NUMBER(10,2)");

            // ── Contratação (Dev 2) ───────────────────────────────
            modelBuilder.Entity<Contratacao>(e =>
            {
                e.Property(c => c.Status).HasConversion<string>();

                e.Property(c => c.TaxaMdrEfetiva)
                 .HasColumnType("NUMBER(10,2)");

                e.HasOne(c => c.Cliente)
                 .WithMany(cl => cl.Contratacoes)
                 .HasForeignKey(c => c.ClienteId)
                 .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(c => c.Produto)
                 .WithMany(p => p.Contratacoes)
                 .HasForeignKey(c => c.ProdutoId)
                 .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
