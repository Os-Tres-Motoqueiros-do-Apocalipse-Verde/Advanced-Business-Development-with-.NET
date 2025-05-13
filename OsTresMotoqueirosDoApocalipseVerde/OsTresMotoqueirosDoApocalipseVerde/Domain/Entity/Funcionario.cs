using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace OsTresMotoqueirosDoApocalipseVerde.Domain.Entity
{
    public class Funcionario
    {
        public Guid IdFuncionario { get; private set; }

        public Cargo Cargo { get; private set; }

        public Filial IdFilial { get; private set; }

        public Dados DadosCpf { get; private set; }

        public Funcionario(Cargo cargo, Filial idFilial, Dados dadosCpf)
        {
            IdFuncionario = Guid.NewGuid();
            Cargo = cargo;
            IdFilial = idFilial;
            DadosCpf = dadosCpf;
        }
        
        public void AtribuirDados(string cpf, string telefone, string email, string nome)
        {
            if (DadosCpf != null)
                throw new InvalidOperationException("Este funcionário já possui dados.");

            DadosCpf = Dados.Create(cpf, telefone, email, nome, funcionario: this);
        }


        internal static Funcionario Create(Cargo cargo, Filial idFilial, Dados dadosCpf)
        {
            return new Funcionario(cargo, idFilial, dadosCpf);
        }


        public Funcionario()
        {

        }
    }
}
