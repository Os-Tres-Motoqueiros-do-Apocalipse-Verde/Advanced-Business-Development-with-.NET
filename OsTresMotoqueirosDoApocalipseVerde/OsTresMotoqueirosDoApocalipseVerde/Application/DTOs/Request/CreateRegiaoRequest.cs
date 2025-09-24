using NetTopologySuite.Geometries;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request
{
    public class CreateRegiaoRequest
    {
        public Point Localizacao {  get; set; }
        public double Area { get; set; }

    }
}
