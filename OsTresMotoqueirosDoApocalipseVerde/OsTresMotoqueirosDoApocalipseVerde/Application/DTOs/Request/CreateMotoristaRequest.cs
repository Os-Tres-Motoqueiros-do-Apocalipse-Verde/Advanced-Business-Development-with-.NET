using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request
{
    public class CreateMotoristaRequest
    {
        public CreateDadosRequest Dados {  get; set; }

        [EnumDataType(typeof(Plano))]
        public Plano Plano { get; set; }
    }
}
