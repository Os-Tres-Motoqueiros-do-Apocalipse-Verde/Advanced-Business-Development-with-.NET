using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OsTresMotoqueirosDoApocalipseVerde.Domain.Entities
{
    public class Patio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public int TotalMotos { get; set; }
        public int CapacidadeMoto { get; set; }

        // Chave estrangeira
        public long? FilialId { get; set; }
        public virtual Filial Filial { get; set; }

        public long? RegiaoId { get; set; }
        public virtual Regiao Regiao { get; set; }

        private Patio(int totalMotos, int capacidadeMoto, long? filialId, long? regiaoId)
        {
            TotalMotos = totalMotos;
            CapacidadeMoto = capacidadeMoto;
            FilialId = filialId;
            RegiaoId = regiaoId;
        }

        public void Atualizar(int totalMotos, int capacidadeMoto, long? filialId, long? regiaoId)
        {
            TotalMotos = totalMotos;
            CapacidadeMoto = capacidadeMoto;
            FilialId = filialId;
            RegiaoId = regiaoId;
        }


        internal static Patio Create(int totalMotos, int capacidadeMoto, long? filialId, long? regiaoId)
        {
            return new Patio(totalMotos, capacidadeMoto, filialId, regiaoId);
        }

        public Patio() { }
    }
}
