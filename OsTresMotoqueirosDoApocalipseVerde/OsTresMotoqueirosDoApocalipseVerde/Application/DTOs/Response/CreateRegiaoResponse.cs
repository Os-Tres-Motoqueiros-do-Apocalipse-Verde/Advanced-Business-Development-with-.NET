using NetTopologySuite.Geometries;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response
{
    public class CreateRegiaoResponse
    {
        public long? Id { get; set; }
        public Geometry Localizacao { get; set; }
        public double Area { get; set; }
    }
}
