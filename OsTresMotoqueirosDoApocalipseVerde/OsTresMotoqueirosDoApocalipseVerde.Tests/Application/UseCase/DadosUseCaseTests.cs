using FluentAssertions;
using Moq;
using OsTresMotoqueirosDoApocalipseVerde;
using OsTresMotoqueirosDoApocalipseVerde.Application.UseCase;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace OsTresMotoqueirosDoApocalipseVerde.Tests.Application.UseCase
{
    public class DadosUseCaseTests
    {
        [Fact(DisplayName = "Deve chamar o repositório ao buscar todos os dados")]
        public async Task Deve_Chamar_Repositorio_Ao_Buscar_Dados()
        {
            // Arrange
            var mockRepo = new Mock<IRepository<Dados>>();
            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Dados>
            {
                new Dados { Id = 1, Nome = "Teste" }
            });

            var useCase = new DadosUseCase(mockRepo.Object);

            // Act
            var resultado = await useCase.GetAllDadosAsync();

            // Assert
            resultado.Should().NotBeEmpty();
            resultado.Should().ContainSingle(d => d.Nome == "Teste");

            mockRepo.Verify(r => r.GetAllAsync(), Times.Once);
        }
    }
}
