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

        public Dados DadosCpf { get; private set; }

        public Motorista(string plano, Dados dadosCpf)
        {
            IdMotorista = Guid.NewGuid();
            Plano = plano;
            DadosCpf = dadosCpf;
        }
        
        public void AtribuirDados(string cpf, string telefone, string email, string nome)
        {
            if (DadosCpf != null)
                throw new InvalidOperationException("Este motorista já possui dados.");

            DadosCpf = Dados.Create(cpf, telefone, email, nome, motorista: this);
        }


        internal static Motorista Create(string plano, Dados dadosCpf)
        {
            return new Motorista(plano, dadosCpf);
        }


        public Motorista()
        {

        }
    }
}
