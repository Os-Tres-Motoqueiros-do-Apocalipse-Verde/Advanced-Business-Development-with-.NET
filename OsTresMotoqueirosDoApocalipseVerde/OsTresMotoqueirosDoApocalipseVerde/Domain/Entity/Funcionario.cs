using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace OsTresMotoqueirosDoApocalipseVerde.Domain.Entity
{
    public class Funcionario
    {
        public int IdFuncionario { get; private set; }

        public Cargo Cargo { get; private set; }

        //Relacionamento
        public int FilialId { get; private set; }
        public virtual Filial Filial { get; private set; }

        public int DadosId { get; private set; }
        public virtual Dados Dados { get; private set; }

        public Funcionario(Cargo cargo, int filialId, int dadosId)
        {
            Cargo = cargo;
            FilialId = filialId;
            DadosId = dadosId;
        }
        
        public void AtribuirDados(string cpf, string telefone, string email, string senha, string nome)
        {
            if (Dados != null)
                throw new InvalidOperationException("Este funcionário já possui dados.");

            Dados = Dados.Create(cpf, telefone, email, senha, nome);
        }


        internal static Funcionario Create(Cargo cargo, int filialId, int dadosId)
        {
            return new Funcionario(cargo, filialId, dadosId);
        }


        public Funcionario()
        {

        }
    }
}
