using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OsTresMotoqueirosDoApocalipseVerde.Domain.Entities
{
    public class Setor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public int QtdMoto { get; set; }
        public int Capacidade { get; set; }

        public string NomeSetor { get; set; }
        public string Descricao { get; set; }
        public string Cor {  get; set; }

        // Chave estrangeira
        public long? PatioId { get; set; }
        public virtual Patio Patio { get; set; }

        public long? RegiaoId { get; set; }
        public virtual Regiao Regiao { get; set; }

        private Setor(int qtdMoto, int capacidade, string nomeSetor, string descricao, string cor, long? patioId, long? regiaoId)
        {
            QtdMoto = qtdMoto;
            Capacidade = capacidade;
            NomeSetor = nomeSetor;
            Descricao = descricao;
            PatioId = patioId;
            RegiaoId = regiaoId;
        }

        public void Atualizar(int qtdMoto, int capacidade, string nomeSetor, string descricao, string cor, long? patioId, long? regiaoId)
        {
            QtdMoto = qtdMoto;
            Capacidade = capacidade;
            NomeSetor = nomeSetor;
            Descricao = descricao;
            PatioId = patioId;
            RegiaoId = regiaoId;
        }


        internal static Setor Create(int qtdMoto, int capacidade, string nomeSetor, string descricao, string cor, long? patioId, long? regiaoId)
        {
            return new Setor(qtdMoto, capacidade, nomeSetor, descricao, cor, patioId, regiaoId);
        }

        public Setor() { }
    }
}
