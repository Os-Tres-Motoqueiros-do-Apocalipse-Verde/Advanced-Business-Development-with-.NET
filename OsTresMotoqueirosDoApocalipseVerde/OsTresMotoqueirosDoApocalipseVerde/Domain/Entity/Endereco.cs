using OsTresMotoqueirosDoApocalipseVerde.Domain.Exceptions;

namespace OsTresMotoqueirosDoApocalipseVerde.Domain.Entity
{
    public class Endereco
    {
        public Guid IdEndereco { get; private set; }

        public int Numero { get; private set; }

        public string Estado { get; private set; }

        public string Pais { get; set; }

        public string Complemento { get; set; }

        public string Rua { get; set; }

        public Endereco(int numero, string estado, string pais, string complemento, string rua)
        {
            IdEndereco = Guid.NewGuid();
            Numero = numero;
            Estado = estado ?? throw new DomainException($"Endereco é obrigatorio");
            Pais = pais ?? throw new DomainException($"Pais é obrigatorio");
            Complemento = complemento;
            Rua = rua ?? throw new DomainException($"Rua é obrigatorio");
        }

        private void ValidadorNumero(int numero)
        {
            if (numero < 1)
                throw new DomainException($"Numero incorreto: {numero}. Verifique antes de criar.");
        }

        internal static Endereco Create(int numero, string estado, string pais, string complemento, string rua)
        {
            return new Endereco(numero, estado, pais, complemento, rua);
        }


        public Endereco()
        {

        }
    }
}
