using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entities;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.DTOs
{
    public class ReadMotoristaDto
    {
        public int Id { get; set; }
        public Plano Plano { get; set; }

        public ReadDadosDto Dados { get; set; }
    }
}
