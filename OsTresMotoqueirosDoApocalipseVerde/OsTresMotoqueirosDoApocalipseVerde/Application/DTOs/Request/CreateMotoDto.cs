using System.ComponentModel.DataAnnotations;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.DTOs
{
    public class CreateMotoDto
    {
        [Required]
        [RegularExpression(@"^[A-Z]{3}[0-9][A-Z0-9][0-9]{2}$",
       ErrorMessage = "Placa inválida. Deve seguir o padrão Mercosul, ex: BRA2E19")]
        public string Placa { get; set; }
        public string Chassi { get; set; }

    }
}
