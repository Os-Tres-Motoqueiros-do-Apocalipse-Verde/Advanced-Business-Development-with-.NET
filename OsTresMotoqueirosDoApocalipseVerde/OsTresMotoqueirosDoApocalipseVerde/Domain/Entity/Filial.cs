using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;


namespace OsTresMotoqueirosDoApocalipseVerde.Domain.Entity
{
    public class Filial
    {
        [Key]
        public int IdFilial { get; private set; }

        public string NomeFilial { get; private set; }
        
        public virtual int ResponsavelId { get; private set; }
        

        //Relacionamento 1..N
        private readonly List<Funcionario> _funcionarios = new();
        public virtual IReadOnlyCollection<Funcionario> Funcionarios => _funcionarios.AsReadOnly();

        
        public int EnderecoId { get; private set; }
        public virtual Endereco Endereco { get; private set; }

        public Filial(string nomeFilial, int responsavelId, int enderecoId)
        {
            NomeFilial = nomeFilial;
            ResponsavelId = responsavelId;
            EnderecoId = enderecoId;
        }

        public void ResponsavelGerente()
        {
            var gerente = _funcionarios.SingleOrDefault(f => f.Cargo == Cargo.Gerente);

            if (gerente == null)
                throw new DomainException("Nenhum funcionário com cargo de Gerente foi encontrado na filial.");

            ResponsavelId = gerente.IdFuncionario;
        }


        public Funcionario AddFuncionario(Cargo cargo, int filialId, int dadosId){ 
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
        

        internal static Filial Create(string nomeFilial, int responsavelId, int enderecoId)
        {

            return new Filial(nomeFilial, responsavelId , enderecoId);
        }



        public Filial()
        {
            _funcionarios = new List<Funcionario>();
        }
    }
}
