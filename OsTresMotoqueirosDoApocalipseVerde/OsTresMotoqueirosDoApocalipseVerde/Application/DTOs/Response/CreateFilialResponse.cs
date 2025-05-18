namespace OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response
{
    public class CreateFilialResponse
    {
        public int Id { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }

        public string Senha { get; set; }
        public string Nome { get; set; }

        public int FuncionarioId { get; set; }
        public int MotoristaId { get; set; }
    }
}
