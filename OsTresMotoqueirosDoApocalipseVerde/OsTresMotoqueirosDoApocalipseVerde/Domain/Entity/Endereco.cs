using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OsTresMotoqueirosDoApocalipseVerde.Domain.Entity
{
    public class Endereco
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }


        public int Numero { get; set; }
        public string Estado { get; set; }
        public string CodigoPais { get; set; }
        public string CodigoPostal { get; set; }
        public string? Complemento { get; set; }
        public string Rua { get; set; }

        public virtual Filial Filial { get; set; }

        private Endereco(int numero, string estado, string codigoPais, string codigoPostal, string complemento, string rua)
        {
            Numero = numero;
            Estado = estado;
            CodigoPais = codigoPais;
            CodigoPostal = codigoPostal;
            Complemento = complemento;
            Rua = rua;
        }

        public void Atualizar(int numero, string estado, string codigoPais, string codigoPostal, string complemento, string rua)
        {
            Numero = numero;
            Estado = estado;
            CodigoPais = codigoPais;
            CodigoPostal = codigoPostal;
            Complemento = complemento;
            Rua = rua;
        }


        internal static Endereco Create(int numero, string estado, string codigoPais, string codigoPostal, string complemento, string rua)
        {
            return new Endereco(numero, estado, codigoPais, codigoPostal, complemento, rua);
        }


        public Endereco() { }
    }
}
