namespace OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response
{
    public class CreateFilialResponse
    {
        public long Id { get; set; }
        public string NomeFilial { get; set; }

        // chave estrangeira
        public CreateEnderecoResponse Endereco { get; set; }
    }
}
