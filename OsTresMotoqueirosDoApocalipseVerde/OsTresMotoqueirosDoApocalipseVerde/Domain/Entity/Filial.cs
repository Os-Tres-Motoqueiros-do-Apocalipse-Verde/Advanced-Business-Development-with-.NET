using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace OsTresMotoqueirosDoApocalipseVerde.Domain.Entity
{
    public class Filial
    {
        public Guid IdFilial { get; private set; }

        public string NomeFilial { get; private set; }
        
        public Guid ResponsavelId { get; private set; }
        public virtual Funcionario Responsavel { get; private set; }
        

        //Relacionamento 1..N
        private readonly List<Funcionario> _funcionarios = new();
        public virtual IReadOnlyCollection<Funcionario> Funcionarios => _funcionarios.AsReadOnly();

        public Guid PatioId { get; private set; }
        public virtual Patio Patio { get; private set; }
        
        public Guid EnderecoId { get; private set; }
        public virtual Endereco Endereco { get; private set; }

        public Filial(string nomeFilial, Guid responsavelId, Guid patioId, Guid enderecoId)
        {
            IdFilial = Guid.NewGuid();
            NomeFilial = nomeFilial;
            ResponsavelId = responsavelId;
            PatioId = patioId;
            EnderecoId = enderecoId;
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
        

        internal static Filial Create(string nomeFilial, Funcionario responsavel, Guid patioId, Guid enderecoId)
        {
            if (responsavel.Cargo != Cargo.Gerente)
                throw new DomainException("O responsável pela filial deve ter o cargo de Gerente.");

            return new Filial(nomeFilial, responsavel.IdFuncionario, patioId, enderecoId);
        }



        public Filial()
        {
            _funcionarios = new List<Funcionario>();
        }
    }
}
