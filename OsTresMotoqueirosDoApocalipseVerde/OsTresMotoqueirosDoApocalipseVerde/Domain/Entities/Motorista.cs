using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OsTresMotoqueirosDoApocalipseVerde.Domain.Entities
{
    public class Motorista
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public Plano Plano { get; set; }

        // Chave estrangeira
        public long? DadosId { get; set; }
        public virtual Dados Dados { get; set; }

        private Motorista(Plano plano, long? dadosId)
        {
            Plano = plano;
            DadosId = dadosId;
        }

        public void Atualizar(Plano plano, long? dadosId)
        {
            Plano = plano;
            DadosId = dadosId;
        }


        internal static Motorista Create(Plano plano, long? dadosId)
        {
            return new Motorista(plano, dadosId);
        }

        public Motorista() { }
    }

}
