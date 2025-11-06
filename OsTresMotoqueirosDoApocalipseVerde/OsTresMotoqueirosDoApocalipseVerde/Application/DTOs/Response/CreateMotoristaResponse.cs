using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response
{
    public class CreateMotoristaResponse
    {
        public long Id { get; set; }

        [EnumDataType(typeof(Plano))]
        public Plano Plano { get; set; }


        public CreateDadosResponse Dados { get; set; }
    }
}
