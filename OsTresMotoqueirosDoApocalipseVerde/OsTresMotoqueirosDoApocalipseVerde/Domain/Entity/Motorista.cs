using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Exceptions;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace OsTresMotoqueirosDoApocalipseVerde.Domain.Entity
{
    public class Motorista
    {
        public int IdMotorista { get; private set; }

        public Plano Plano { get; private set; }

        // Relacionamento 
        public int DadosId { get; private set; }
        public virtual Dados Dados { get; private set; }

        public Motorista(Plano plano, int dadosId)
        {
            Plano = plano;
            DadosId = dadosId;
        }       
        
        public void AtribuirDados(string cpf, string telefone, string email, string senha, string nome)
        {
            if (Dados != null)
                throw new InvalidOperationException("Este motorista já possui dados.");

            Dados = Dados.Create(cpf, telefone, email, senha, nome);
        }


        internal static Motorista Create(Plano plano, int dadosId)
        {
            return new Motorista(plano, dadosId);
        }


        public Motorista()
        {

        }
    }
}
