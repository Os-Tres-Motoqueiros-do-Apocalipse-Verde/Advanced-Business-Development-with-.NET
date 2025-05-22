using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request
{
    public class UpdateMotoristaDto
    {
        public Plano Plano { get; set; }
        public int? DadosId { get; set; }
    }
}