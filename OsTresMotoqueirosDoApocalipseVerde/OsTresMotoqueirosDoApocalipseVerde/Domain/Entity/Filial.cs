using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace OsTresMotoqueirosDoApocalipseVerde.Domain.Entity
{
    public class Filial
    {
        public Guid IdFilial { get; private set; }

        public string NomeFilial { get; private set; }

        public Funcionario IdResponsavel { get; set; }

        //Relacionamento 1..N
        private readonly List<Funcionario> _funcionarios = new();
        public virtual IReadOnlyCollection<Funcionario> Funcionarios => _funcionarios.AsReadOnly();

        public Patio Patio { get; set; }

        public Endereco Endereco { get; set; }

        public Filial(string nomeFilial, Funcionario idResposavel, Patio patio, Endereco endereco)
        {
            IdFilial = Guid.NewGuid();
            NomeFilial = nomeFilial;
            IdResponsavel = idResposavel;
            Patio = patio;
            Endereco = endereco;
        }

        public Funcionario AddFuncionario(Cargo cargo, Guid filialId, Guid dadosId){ 
            var funcionario = Funcionario.Create(cargo, filialId, dadosId);
            _funcionarios.Add(funcionario);

            return funcionario;
        }

        public void AtribuirEndereco(int numero, string estado, string codigoPais, string codigoPostal, string complemento, string rua)
        {
            if (Endereco != null)
                throw new InvalidOperationException("Esta Filial já possui endereco.");

            Endereco = Endereco.Create(numero, estado, codigoPais, codigoPostal, complemento, rua, filial: this);
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

        internal static Filial Create(string nomeFilial, Funcionario idResponsavel, Patio idPatio, Endereco endereco)
        {
            if (idResponsavel.Cargo != Cargo.Gerente)
                throw new DomainException("O responsável pela filial deve ter o cargo de Gerente.");

            return new Filial(nomeFilial, idResponsavel, idPatio, endereco);
        }


        public Filial()
        {
            _funcionarios = new List<Funcionario>();
        }
    }
}
