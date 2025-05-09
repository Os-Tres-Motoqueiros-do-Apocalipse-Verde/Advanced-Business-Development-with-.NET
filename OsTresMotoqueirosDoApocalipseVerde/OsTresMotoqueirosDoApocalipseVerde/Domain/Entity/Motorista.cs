using OsTresMotoqueirosDoApocalipseVerde.Domain.Exceptions;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace OsTresMotoqueirosDoApocalipseVerde.Domain.Entity
{
    public class Motorista
    {
        public Guid Id_motorista { get; private set; }

        public string Plano { get; private set; }

        public Dados Dados_cpf { get; private set; }

        public Motorista(string plano, Dados dados_cpf)
        {
            Id_motorista = Guid.NewGuid();
            Plano = plano;
            Dados_cpf = dados_cpf;
        }

        internal static Motorista Create(string plano, Dados dados_cpf)
        {
            return new Motorista(plano, dados_cpf);
        }


        public Motorista()
        {

        }
    }
}
