namespace OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request
{
    public class CreateFilialRequest
    {

        public string NomeFilial { get; set; }

        // chave estrangeira
        public CreateEnderecoRequest Endereco { get; set; }

    }
}
