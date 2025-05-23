using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OsTresMotoqueirosDoApocalipseVerde.Domain.Entities
{
    public class Motorista
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [EnumDataType(typeof(Plano))]
        public Plano Plano { get; set; }

        // Foreign key
        [ForeignKey("Dados")]
        public int? DadosId { get; set; }
        public virtual Dados Dados { get; set; }
    }

}
