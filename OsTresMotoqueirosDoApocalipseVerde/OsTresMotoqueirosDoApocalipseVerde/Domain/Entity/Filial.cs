using OsTresMotoqueirosDoApocalipseVerde.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace OsTresMotoqueirosDoApocalipseVerde.Domain.Entity
{
    public class Filial
    {
        public Guid IdFilial { get; private set; }

        public string Nome { get; private set; }

        public int TotalMotos { get; private set; }

        public int CapacidadeMoto { get; set; }

        public int AreaPatio { get; set; }

        public Funcionario IdResponsavel { get; set; }

        public Setor IdSetor { get; set; }

        public Endereco Endereco { get; set; }

        public Filial(string nome, int totalMotos, int capacidadeMoto, int areaPatio, Funcionario idResposavel, Setor idSetor, Endereco endereco)
        {
            IdFilial = Guid.NewGuid();
            Nome = nome;
            TotalMotos = totalMotos;
            CapacidadeMoto = capacidadeMoto;
            AreaPatio = areaPatio;
            IdResponsavel = idResposavel;
            IdSetor = idSetor;
            Endereco = endereco;
        }

        private void VerificadorCapacidade(int capacidadeMoto)
        {
            if (capacidadeMoto > 10)
                throw new DomainException($"A capacidade do patio esta errada {capacidadeMoto}, verifique antes de criar");
        }

        private void VerificadorArea(int areaPatio)
        {
            if (areaPatio > 10)
                throw new DomainException($"A area do patio esta errada: {areaPatio}, verifique antes de criar");
        }

        internal static Filial Create(string nome, int totalMotos, int capacidadeMoto, int areaPatio, Funcionario idResponsavel, Setor idSetor, Endereco endereco)
        {
            return new Filial(nome, totalMotos, capacidadeMoto, areaPatio, idResponsavel, idSetor, endereco);
        }


        public Filial()
        {

        }
    }
}
