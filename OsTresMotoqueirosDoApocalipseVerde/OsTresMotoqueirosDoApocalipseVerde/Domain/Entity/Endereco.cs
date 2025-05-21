using OsTresMotoqueirosDoApocalipseVerde.Domain.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace OsTresMotoqueirosDoApocalipseVerde.Domain.Entity
{
    public class Endereco
    {
        [Key]
        public int IdEndereco { get; private set; }

        public int Numero { get; private set; }

        public string Estado { get; private set; }

        public string CodigoPais { get; set; }

        public string CodigoPostal { get; set; }

        public string Complemento { get; set; }

        public string Rua { get; set; }

        public int? FilialId { get; private set; }
        public virtual Filial Filial { get; private set; }

        public Endereco(int numero, string estado, string codigoPais, string codigoPostal, string complemento, string rua)
        {
            Numero = numero;
            Estado = estado ?? throw new DomainException($"Endereco é obrigatorio");
            CodigoPais = codigoPais ?? throw new DomainException($"O codigo do pais é obrigatorio");
            CodigoPostal = codigoPostal ?? throw new DomainException($"O codigo postal é obrigatorio");
            Complemento = complemento;
            Rua = rua ?? throw new DomainException($"Rua é obrigatorio");
         
        }

        private void ValidadorNumero(int numero)
        {
            if (numero < 1)
                throw new DomainException($"Numero incorreto: {numero}. Verifique antes de criar.");
        }

        internal static Endereco Create(int numero, string estado, string codigoPais, string codigoPostal, string complemento, string rua, Filial filial)
        {
            return new Endereco(numero, estado, codigoPais, codigoPostal, complemento, rua);
        }


        public Endereco()
        {

        }
    }
}
