using Microsoft.ML;
using OsTresMotoqueirosDoApocalipseVerde.Application.ML;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entity;
using System.Diagnostics;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.UseCase
{
    public class MotoPredictionUseCase
    {
        private readonly IRepository<Moto> _repository;
        private readonly MLContext _mlContext;
        private readonly string _modelPath = Path.Combine(AppContext.BaseDirectory, "motoModel.zip");

        public MotoPredictionUseCase(IRepository<Moto> repository)
        {
            _repository = repository;
            _mlContext = new MLContext(seed: 42);
        }

        // Método que TREINA o modelo
        public async Task<string> TreinarModeloAsync()
        {
            Console.WriteLine("Iniciando treinamento do modelo...");

            var motos = await _repository.GetAllAsync();
            var dadosTreino = motos
                .Where(m => !string.IsNullOrEmpty(m.Condicao))
                .Select(m => new MotoConditionData
                {
                    ModeloId = m.ModeloId,
                    SetorId = m.SetorId,
                    SituacaoId = m.SituacaoId,
                    Condicao = m.Condicao
                })
                .ToList();

            if (!dadosTreino.Any())
                throw new InvalidOperationException("Nenhum dado válido encontrado para treino.");

            Console.WriteLine($"Total de registros para treino: {dadosTreino.Count}");

            var data = _mlContext.Data.LoadFromEnumerable(dadosTreino);

            // Pipeline base com cache
            var basePipeline = _mlContext.Transforms.Conversion.MapValueToKey("Label", "Condicao")
                .Append(_mlContext.Transforms.Concatenate("Features", "ModeloId", "SetorId", "SituacaoId"))
                .AppendCacheCheckpoint(_mlContext);

            // Trainer primário (rápido e estável)
            var trainerPrincipal = _mlContext.MulticlassClassification.Trainers.LbfgsMaximumEntropy();

            // Pipeline completo
            var pipeline = basePipeline
                .Append(trainerPrincipal)
                .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            return await Task.Run(() =>
            {
                var stopwatch = Stopwatch.StartNew();
                Console.WriteLine("Treinando modelo...");

                ITransformer model = null;
                try
                {
                    model = pipeline.Fit(data);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro com L-BFGS: {ex.Message}");
                    Console.WriteLine("Tentando fallback com NaiveBayes...");

                    // Fallback ultra-rápido
                    var fallbackPipeline = basePipeline
                        .Append(_mlContext.MulticlassClassification.Trainers.NaiveBayes())
                        .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

                    model = fallbackPipeline.Fit(data);
                }

                stopwatch.Stop();
                var tempo = stopwatch.Elapsed.TotalSeconds;
                Console.WriteLine($"Modelo treinado em {tempo:F2} segundos.");

                _mlContext.Model.Save(model, data.Schema, _modelPath);
                Console.WriteLine($"Modelo salvo em: {_modelPath}");

                return $"Modelo treinado e salvo em: {_modelPath} (Tempo: {tempo:F2}s)";
            });
        }

        // Método para prever a condição
        public string PreverCondicao(float modeloId, float setorId, float situacaoId)
        {
            if (!File.Exists(_modelPath))
                throw new FileNotFoundException("O modelo ainda não foi treinado.");

            var model = _mlContext.Model.Load(_modelPath, out var schema);
            var predictionEngine = _mlContext.Model.CreatePredictionEngine<MotoConditionData, MotoConditionPrediction>(model);

            var input = new MotoConditionData
            {
                ModeloId = modeloId,
                SetorId = setorId,
                SituacaoId = situacaoId
            };

            var prediction = predictionEngine.Predict(input);
            return $"Predição: {prediction.PredictedCondicao}";
        }
    }
}
