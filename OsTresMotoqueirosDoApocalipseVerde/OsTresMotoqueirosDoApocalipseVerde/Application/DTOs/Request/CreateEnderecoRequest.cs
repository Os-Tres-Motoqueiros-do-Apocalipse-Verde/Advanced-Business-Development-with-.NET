namespace OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request
{
    public class CreateEnderecoRequest
    {
        public int Numero { get; set; }
        public string Estado { get; set; }
        public string CodigoPais { get; set; }
        public string CodigoPostal { get; set; }
        public string Complemento { get; set; }
        public string Rua { get; set; }
    }
}
