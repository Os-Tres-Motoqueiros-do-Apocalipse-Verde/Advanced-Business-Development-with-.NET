using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OsTresMotoqueirosDoApocalipseVerde.Domain.Entities
{
    public class Modelo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string NomeModelo { get; set; }

        public Frenagem Frenagem { get; set; }

        public SistemaPartida SistemaPartida { get; set; }

        [Required]
        public int Tanque { get; set; }

        [Required]
        public TipoCombustivel TipoCombustivel { get; set; }

        [Required]
        public int Consumo { get; set; }

    }
}
