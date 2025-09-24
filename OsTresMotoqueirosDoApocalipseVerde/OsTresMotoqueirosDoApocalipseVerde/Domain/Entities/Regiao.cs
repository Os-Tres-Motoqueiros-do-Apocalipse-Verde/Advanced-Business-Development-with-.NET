using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OsTresMotoqueirosDoApocalipseVerde.Domain.Entities
{
    public class Regiao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        
        public Point Localizacao { get; set; }
        public double Area { get; private set; }

        public virtual Patio Patio { get; set; }
        public virtual Setor Setor { get; set; }

        private Regiao(Point localizacao)
        {
            Localizacao = localizacao;
        }

        public void Atualizar(Point localizacao)
        {
            Localizacao = localizacao;
        }


        internal static Regiao Create(Point localizacao)
        {
            return new Regiao(localizacao);
        }

        public Regiao() { }
    }
}
