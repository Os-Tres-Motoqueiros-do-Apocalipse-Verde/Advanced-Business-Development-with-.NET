using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response
{
    public class CreateUsuariosResponse
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}
