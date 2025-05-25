using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OsTresMotoqueirosDoApocalipseVerde.Domain.Entities
{
    public class Moto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Placa { get; set; }

        [Required]
        public string Chassi { get; set; }

        public string Condicao { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        [ForeignKey("Modelo")]
        public int? ModeloId { get; set; }
        public virtual Modelo Modelo { get; set; }
    }
}
