using ProjectBank.Models;

namespace ProjectBank.Services
{
    // Regra de negócio extra (requisito de dupla):
    // PJ recebe 30% de desconto na taxa MDR
    // PF paga a taxa cheia
    public interface IMdrService
    {
        decimal CalcularTaxaEfetiva(MaquinaDeCartao maquina, Cliente cliente);
    }

    public class MdrService : IMdrService
    {
        private const decimal DescontoPj = 0.30m;

        public decimal CalcularTaxaEfetiva(MaquinaDeCartao maquina, Cliente cliente)
        {
            if (cliente is PessoaJuridica)
                return Math.Round(maquina.TaxaMdrBase * (1 - DescontoPj), 2);

            return maquina.TaxaMdrBase;
        }
    }
}
