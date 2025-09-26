using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OsTresMotoqueirosDoApocalipseVerde.Domain.Entity
{
    public class Funcionario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public Cargo Cargo { get; set; }

        // Chave estrangeira
        public long DadosId { get; set; }
        public virtual Dados Dados { get; set; }

        public long FilialId { get; set; }
        public virtual Filial Filial { get; set; }

        private Funcionario(Cargo cargo, long dadosId, long filialId)
        {
            Cargo = cargo;
            DadosId = dadosId;
            FilialId = filialId;
        }

        public void Atualizar(Cargo cargo, long dadosId, long filialId)
        {
            Cargo = cargo;
            DadosId = dadosId;
            FilialId = filialId;
        }


        internal static Funcionario Create(Cargo cargo, long dadosId, long filialId)
        {
            return new Funcionario(cargo, dadosId, filialId);
        }

        public Funcionario() { }
    }
}
