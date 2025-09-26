using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OsTresMotoqueirosDoApocalipseVerde.Domain.Entity
{
    public class Patio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public int TotalMotos { get; set; }
        public int CapacidadeMoto { get; set; }

        public string Localizacao { get; set; }

        // Chave estrangeira
        public long FilialId { get; set; }
        public virtual Filial Filial { get; set; }


        public virtual ICollection<Setor> Setores { get; private set; } = new List<Setor>();

        private Patio(int totalMotos, int capacidadeMoto, string localizacao, long filialId)
        {
            TotalMotos = totalMotos;
            CapacidadeMoto = capacidadeMoto;
            Localizacao = localizacao;
            FilialId = filialId;

        }

        public void Atualizar(int totalMotos, int capacidadeMoto, string localizacao, long filialId)
        {
            TotalMotos = totalMotos;
            CapacidadeMoto = capacidadeMoto;
            Localizacao = localizacao;
            FilialId = filialId;
        }


        internal static Patio Create(int totalMotos, int capacidadeMoto, string localizacao, long filialId)
        {
            return new Patio(totalMotos, capacidadeMoto, localizacao, filialId);
        }

        public Patio() { }
    }
}
