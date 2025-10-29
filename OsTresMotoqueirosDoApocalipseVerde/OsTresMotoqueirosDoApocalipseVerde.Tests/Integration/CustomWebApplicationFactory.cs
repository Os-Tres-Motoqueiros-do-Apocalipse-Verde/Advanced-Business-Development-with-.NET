using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

namespace OsTresMotoqueirosDoApocalipseVerde.Tests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            // Aqui você pode configurar serviços específicos para testes se quiser
            return base.CreateHost(builder);
        }
    }
}
