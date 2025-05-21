using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Exceptions;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OsTresMotoqueirosDoApocalipseVerde.Domain.Entity
{
    public class Motorista
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [EnumDataType(typeof(Plano))]
        public Plano Plano { get; set; }

        // Relacionamento 
        [ForeignKey("Dados")]
        public int? DadosId { get;  set; }
        public virtual Dados Dados { get; set; }

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
