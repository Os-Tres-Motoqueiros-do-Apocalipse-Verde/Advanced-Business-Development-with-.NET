
using System.ComponentModel.DataAnnotations;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request
{
    public class CreateMotoristaDto
    {
     
        public Plano Plano { get; set; }

        public int? DadosId { get; set; }
        public CreateDadosDto Dados { get; set; }
    }
}