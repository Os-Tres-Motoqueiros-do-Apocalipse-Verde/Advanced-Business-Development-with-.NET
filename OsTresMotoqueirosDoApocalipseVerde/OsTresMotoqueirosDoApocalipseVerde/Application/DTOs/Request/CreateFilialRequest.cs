namespace OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request
{
    public class CreateFilialRequest
    {

        public string NomeFilial { get; set; }

        // chave estrangeira
        public long EnderecoId { get; set; }

    }
}
