using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OsTresMotoqueirosDoApocalipseVerde.Domain.Entity
{
    public class Moto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Placa { get; set; }
        public string Chassi { get; set; }

        public string Condicao { get; set; }

        public string LocalizacaoMoto { get; set; }


        // Chaves estrangeiras
        public long MotoristaId { get; set; }
        public virtual Motorista Motorista { get; set; }

        public long ModeloId { get; set; }
        public virtual Modelo Modelo { get; set; }

        public long SetorId { get; set; }
        public virtual Setor Setor { get; set; }

        public long SituacaoId { get; set; }
        public virtual Situacao Situacao { get; set; }



        private Moto(string placa, string chassi, string condicao, string localizacaoMoto, long motoristaId, long modeloId, long setorId, long situacaoId)
        {
            Placa = placa;
            Chassi = chassi;
            Condicao = condicao;
            LocalizacaoMoto = localizacaoMoto;
            MotoristaId = motoristaId;
            ModeloId = modeloId;
            SetorId = setorId;
            SituacaoId = situacaoId;
        }

        public void Atualizar(string placa, string chassi, string condicao, string localizacaoMoto, long motoristaId, long modeloId, long setorId, long situacaoId)
        {
            Placa = placa;
            Chassi = chassi;
            Condicao = condicao;
            LocalizacaoMoto = localizacaoMoto;
            MotoristaId = motoristaId;
            ModeloId = modeloId;
            SetorId = setorId;
            SituacaoId = situacaoId;
        }


        internal static Moto Create(string placa, string chassi, string condicao, string localizacaoMoto, long motoristaId, long modeloId, long setorId, long situacaoId)
        {
            return new Moto(placa, chassi, condicao, localizacaoMoto, motoristaId, modeloId, setorId, situacaoId);
        }

        public Moto() { }
    }
}
