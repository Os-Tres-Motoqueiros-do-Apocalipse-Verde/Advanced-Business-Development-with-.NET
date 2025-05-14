using OsTresMotoqueirosDoApocalipseVerde.Domain.Exceptions;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace OsTresMotoqueirosDoApocalipseVerde.Domain.Entity
{
    public class Motorista
    {
        public Guid IdMotorista { get; private set; }

        public string Plano { get; private set; }

        // Relacionamento 
        public Guid DadosId { get; private set; }
        public virtual Dados DadosCpf { get; private set; }

        public Motorista(string plano, Guid dadosId)
        {
            IdMotorista = Guid.NewGuid();
            Plano = plano;
            DadosId = dadosId;
        }
        
        public void AtribuirDados(string cpf, string telefone, string email, string nome)
        {
            if (DadosCpf != null)
                throw new InvalidOperationException("Este motorista já possui dados.");

            DadosCpf = Dados.Create(cpf, telefone, email, nome, motorista: this);
        }


        internal static Motorista Create(string plano, Guid dadosId)
        {
            return new Motorista(plano, dadosId);
        }


        public Motorista()
        {

        }
    }
}
