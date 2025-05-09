using OsTresMotoqueirosDoApocalipseVerde.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace OsTresMotoqueirosDoApocalipseVerde.Domain.Entity
{
    public class Setor
    {
        public Guid IdSetor { get; private set; }

        public int QuantidadeMoto { get; private set; }

        public int Capacidade { get; private set; }

        public long AreaSetor { get; set; }

        public string NomeSetor { get; set; }

        public string Descricao { get; set; }

        public Setor(int quantidadeMoto, int capacidade, long areaSetor, string nomeSetor, string descricao)
        {
            IdSetor = Guid.NewGuid();
            QuantidadeMoto = quantidadeMoto;
            Capacidade = capacidade;
            AreaSetor = areaSetor;
            NomeSetor = nomeSetor;
            Descricao = descricao;
        }

        private void ValidadorCapacidade(int capacidade)
        {
           
            if (capacidade < 10)
                throw new DomainException($"Capacidade esta incorreta: {capacidade}. Verifique antes de criar.");

        }

        private void ValidadorArea(long areaSetor)
        {

            if (areaSetor < 10)
                throw new DomainException($"A area do Setor esta incorreta: {areaSetor}. Verifique antes de criar.");

        }

        internal static Setor Create(int quantidadeMoto, int capacidade, long areaSetor, string nomeSetor, string descricao)
        {
            return new Setor(quantidadeMoto, capacidade, areaSetor, nomeSetor, descricao);
        }


        public Setor()
        {

        }
    }
}
