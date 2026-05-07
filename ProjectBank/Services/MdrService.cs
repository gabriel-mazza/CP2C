using ProjectBank.Models;

namespace ProjectBank.Services
{
   
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
