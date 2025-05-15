using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace OsTresMotoqueirosDoApocalipseVerde.Domain.Entity
{
    public class Funcionario
    {
        public Guid IdFuncionario { get; private set; }

        public Cargo Cargo { get; private set; }

        //Relacionamento
        public Guid FilialId { get; private set; }
        public Filial Filial { get; private set; }

        public Guid DadosId { get; private set; }
        public virtual Dados Dados { get; private set; }

        public Funcionario(Cargo cargo, Guid filialId, Guid dadosId)
        {
            IdFuncionario = Guid.NewGuid();
            Cargo = cargo;
            FilialId = filialId;
            DadosId = dadosId;
        }
        
        public void AtribuirDados(string cpf, string telefone, string email, string senha, string nome)
        {
            if (Dados != null)
                throw new InvalidOperationException("Este funcionário já possui dados.");

            Dados = Dados.Create(cpf, telefone, email, senha, nome, funcionario: this);
        }


        internal static Funcionario Create(Cargo cargo, Guid filialId, Guid dadosId)
        {
            return new Funcionario(cargo, filialId, dadosId);
        }


        public Funcionario()
        {

        }
    }
}
