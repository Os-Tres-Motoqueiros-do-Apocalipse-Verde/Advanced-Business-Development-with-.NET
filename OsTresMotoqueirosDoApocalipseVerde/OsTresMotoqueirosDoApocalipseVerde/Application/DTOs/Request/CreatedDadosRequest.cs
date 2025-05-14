using OsTresMotoqueirosDoApocalipseVerde.Domain.Entity;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request
{
    public class CreatedDadosRequest
    {
        public Guid Id { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }

        public Guid FuncionarioId { get; set; }
        public Guid MotoristaId { get; set; }
    }
}
