using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OsTresMotoqueirosDoApocalipseVerde.Domain.Entities
{
    public class Motorista
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Plano Plano { get; set; }

        public int? DadosId { get; set; }

        [ForeignKey("DadosId")]
        public virtual Dados Dados { get; set; }
    }
}
