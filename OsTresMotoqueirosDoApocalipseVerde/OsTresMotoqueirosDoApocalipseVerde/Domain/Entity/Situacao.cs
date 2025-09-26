using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OsTresMotoqueirosDoApocalipseVerde.Domain.Entity
{
    public class Situacao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Nome { get; set; }

        public string Descricao { get; set; }

        public Status Status { get; set; }

        public virtual Moto Moto { get; set; }

        private Situacao(string nome, string descricao, Status status)
        {
            Nome = nome;
            Descricao = descricao;
            Status = status;
        }

        public void Atualizar(string nome, string descricao, Status status)
        {
            Nome = nome;
            Descricao = descricao;
            Status = status;
        }


        internal static Situacao Create(string nome, string descricao, Status status)
        {
            return new Situacao(nome, descricao, status);
        }

        public Situacao() { }

    }
}
