namespace OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response
{
    public class CreateFilialResponse
    {
        public string NomeFilial { get; set; }

        // chave estrangeira
        public CreateEnderecoResponse Endereco { get; set; }
    }
}
