using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace OsTresMotoqueirosDoApocalipseVerde.Tests.Integration
{
    public class DadosControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;

        public DadosControllerTests(CustomWebApplicationFactory factory)
        {
            _httpClient = factory.CreateClient();
        }

        [Fact(DisplayName = "GET /api/dados deve retornar 200 OK")]
        public async Task Get_DeveRetornarOk()
        {
            // Act
            var response = await _httpClient.GetAsync("/api/v2/dados");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
