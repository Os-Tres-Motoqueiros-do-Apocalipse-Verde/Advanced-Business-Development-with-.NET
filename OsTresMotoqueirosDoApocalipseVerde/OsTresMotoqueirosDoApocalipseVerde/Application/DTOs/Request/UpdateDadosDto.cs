using OsTresMotoqueirosDoApocalipseVerde.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request
{
    public class UpdateDadosDto
    {
      
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }

    }
}
