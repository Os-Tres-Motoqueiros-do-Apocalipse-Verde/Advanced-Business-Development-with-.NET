using Microsoft.ML.Data;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.ML
{
    // Classe usada como input para treino/predição
    public class MotoConditionData
    {
        // usamos float para features numéricas
        [LoadColumn(0)]
        public float ModeloId { get; set; }

        [LoadColumn(1)]
        public float SetorId { get; set; }

        [LoadColumn(2)]
        public float SituacaoId { get; set; }

        // Label (o que quero prever) — string no dataset
        [LoadColumn(3)]
        public string Condicao { get; set; }
    }

    // Classe de saída da predição
    public class MotoConditionPrediction
    {
        [ColumnName("PredictedLabel")]
        public string PredictedCondicao { get; set; }

        // Scores para cada classe
        public float[] Score { get; set; }
    }
}
