using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OsTresMotoqueirosDoApocalipseVerde.Domain.Entity
{
    public class Usuarios
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }

        private Usuarios(string username, string password, Role role)
        {
            Username = username;
            Password = password;
            Role = role;
        }

        public void Atualizar(string username, string password, Role role)
        {
            Username = username;
            Password = password;
            Role = role;
        }


        internal static Usuarios Create(string username, string password, Role role)
        {
            return new Usuarios(username, password, role);
        }

        public Usuarios() { }
    }
}
