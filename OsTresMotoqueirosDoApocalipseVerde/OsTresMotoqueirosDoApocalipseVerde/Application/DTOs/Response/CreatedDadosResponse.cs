namespace OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response
{
    public class CreatedDadosResponse
    {
        public Guid Id { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }

        public Guid FuncionarioId { get; set; }
        public Guid MotoristaId { get; set; }
    }
}
