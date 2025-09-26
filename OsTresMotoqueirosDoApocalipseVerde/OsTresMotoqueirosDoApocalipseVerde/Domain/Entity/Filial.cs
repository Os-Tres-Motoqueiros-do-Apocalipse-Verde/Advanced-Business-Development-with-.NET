using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OsTresMotoqueirosDoApocalipseVerde.Domain.Entity
{
    public class Filial
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string NomeFilial { get; set; }


        // Chaves estrangeiras
        public long EnderecoId { get; set; }
        public virtual Endereco Endereco { get; set; }

        public virtual ICollection<Funcionario> Funcionarios { get; private set; } = new List<Funcionario>();

        public virtual ICollection<Patio> Patios { get; private set; } = new List<Patio>();

        private Filial(string nomeFilial, long enderecoId)
        {
            NomeFilial = nomeFilial;
            EnderecoId = enderecoId;
        }

        public void Atualizar(string nomeFilial, long enderecoId)
        {
            NomeFilial = nomeFilial;
            EnderecoId = enderecoId;
        }


        internal static Filial Create(string nomeFilial, long enderecoId)
        {
            return new Filial(nomeFilial, enderecoId);
        }

        public Filial() { }
    }
}
