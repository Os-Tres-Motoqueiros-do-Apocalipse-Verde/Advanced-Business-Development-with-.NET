using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace OsTresMotoqueirosDoApocalipseVerde.Domain.Entity
{
    public class Funcionario
    {
        public long IdFuncionario { get; private set; }

        public Cargo Cargo { get; private set; }

        //Relacionamento
        public long FilialId { get; private set; }
        public virtual Filial Filial { get; private set; }

        public long DadosId { get; private set; }
        public virtual Dados Dados { get; private set; }

        public Funcionario(Cargo cargo, long filialId, long dadosId)
        {
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


        internal static Funcionario Create(Cargo cargo, long filialId, long dadosId)
        {
            return new Funcionario(cargo, filialId, dadosId);
        }


        public Funcionario()
        {

        }
    }
}
