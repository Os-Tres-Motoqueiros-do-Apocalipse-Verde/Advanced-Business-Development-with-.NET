namespace OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request
{
    public class CreateFilialRequest
    {
        public long IdFilial { get; set; }
        public string NomeFilial { get; set; }
        public string Estado { get; set; }
        public string CodigoPais { get; set; }
        public string CodigoPostal { get; set; }
        public string Complemento { get; set; }
        public string Rua { get; set; }

        public long FilialId { get; set; }
    }
}
