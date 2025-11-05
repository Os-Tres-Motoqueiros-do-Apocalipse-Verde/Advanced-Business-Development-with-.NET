using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OsTresMotoqueirosDoApocalipseVerde.Domain.Entity
{
    public class Setor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string NomeSetor { get; set; }

        public int QtdMoto { get; set; }
        public int Capacidade { get; set; }

        public string Descricao { get; set; }
        public string Cor { get; set; }

        public string Localizacao { get; set; }

        // Chave estrangeira
        [Column("ID_PATIO")]
        public long PatioId { get; set; }
        public virtual Patio Patio { get; set; }

        public virtual ICollection<Moto> Motos { get; private set; } = new List<Moto>();

        private Setor(string nomeSetor, int qtdMoto, int capacidade, string descricao, string cor, string localizacao, long patioId)
        {
            NomeSetor = nomeSetor;
            QtdMoto = qtdMoto;
            Capacidade = capacidade;
            Descricao = descricao;
            Cor = cor;
            Localizacao = localizacao;
            PatioId = patioId;

        }

        public void Atualizar(string nomeSetor, int qtdMoto, int capacidade, string descricao, string cor, string localizacao, long patioId)
        {
            NomeSetor = nomeSetor;
            QtdMoto = qtdMoto;
            Capacidade = capacidade;
            Descricao = descricao;
            Cor = cor;
            Localizacao = localizacao;
            PatioId = patioId;

        }


        internal static Setor Create(string nomeSetor, int qtdMoto, int capacidade, string descricao, string cor, string localizacao, long patioId)
        {
            return new Setor(nomeSetor, qtdMoto, capacidade, descricao, cor, localizacao, patioId);
        }

        public Setor() { }
    }
}
