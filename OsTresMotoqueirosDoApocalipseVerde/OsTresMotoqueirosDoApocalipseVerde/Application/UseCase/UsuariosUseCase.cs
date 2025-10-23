using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entity;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.UseCase
{
    public class UsuariosUseCase
    {
        private readonly IRepository<Usuarios> _repository;

        public UsuariosUseCase(IRepository<Usuarios> repository)
        {
            _repository = repository;
        }

        public async Task<CreateUsuariosResponse> CreateUsuariosAsync(CreateUsuariosRequest request)
        {
            var usuarios = Usuarios.Create(
                request.Username,
                request.Password,
                request.Role
            );

            await _repository.AddAsync(usuarios);
            await _repository.SaveChangesAsync();

            return new CreateUsuariosResponse
            {
                Id = usuarios.Id,
                Username = usuarios.Username,
                Password = usuarios.Password,
                Role = usuarios.Role
            };
        }

        public async Task<List<CreateUsuariosResponse>> GetAllUsuariosAsync()
        {
            var usuarios = await _repository.GetAllAsync();
            return usuarios.Select(u => new CreateUsuariosResponse
            {
                Id = u.Id,
                Username = u.Username,
                Password = u.Password,
                Role = u.Role
            }).ToList();
        }

        /// <summary>
        /// Retorna os Usuarios paginados.
        /// </summary>
        public async Task<List<CreateUsuariosResponse>> GetAllPagedAsync(int page, int pageSize)
        {
            var usuarios = await _repository.GetAllAsync();

            var paged = usuarios
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new CreateUsuariosResponse
                {
                    Id = u.Id,
                    Username = u.Username,
                    Password = u.Password,
                    Role = u.Role
                })
                .ToList();

            return paged;
        }

        public async Task<CreateUsuariosResponse?> GetByIdAsync(long id)
        {
            var usuarios = await _repository.GetByIdAsync(id);
            if (usuarios == null) return null;

            return new CreateUsuariosResponse
            {
                Id = usuarios.Id,
                Username = usuarios.Username,
                Password = usuarios.Password,
                Role = usuarios.Role
            };
        }

        public async Task<bool> UpdateUsuariosAsync(long id, CreateUsuariosRequest request)
        {
            var usuarios = await _repository.GetByIdAsync(id);
            if (usuarios == null) return false;

            usuarios.Atualizar(
                request.Username,
                request.Password,
                request.Role
            );
            _repository.Update(usuarios);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUsuariosAsync(long id)
        {
            var usuarios = await _repository.GetByIdAsync(id);
            if (usuarios == null) return false;

            _repository.Delete(usuarios);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
