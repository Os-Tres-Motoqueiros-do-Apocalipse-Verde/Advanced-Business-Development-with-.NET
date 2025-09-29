using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entity;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.UseCase
{
    public class EnderecoUseCase
    {
        private readonly IRepository<Endereco> _repository;

        public EnderecoUseCase(IRepository<Endereco> repository)
        {
            _repository = repository;
        }

        public async Task<CreateEnderecoResponse> CreateEnderecoAsync(CreateEnderecoRequest request)
        {
            var endereco = Endereco.Create(
                request.Numero,
                request.Estado,
                request.CodigoPais,
                request.CodigoPostal,
                request.Complemento,
                request.Rua
            );

            await _repository.AddAsync(endereco);
            await _repository.SaveChangesAsync();

            return new CreateEnderecoResponse
            {
                Id = endereco.Id,
                Numero = endereco.Numero,
                Estado = endereco.Estado,
                CodigoPais = endereco.CodigoPais,
                CodigoPostal = endereco.CodigoPostal,
                Complemento = endereco.Complemento,
                Rua = endereco.Rua
            };
        }

        public async Task<List<CreateEnderecoResponse>> GetAllEnderecoAsync()
        {
            var endereco = await _repository.GetAllAsync();
            return endereco.Select(u => new CreateEnderecoResponse
            {
                Id = u.Id,
                Numero = u.Numero,
                Estado = u.Estado,
                CodigoPais = u.CodigoPais,
                CodigoPostal = u.CodigoPostal,
                Complemento = u.Complemento,
                Rua = u.Rua
            }).ToList();
        }

        /// <summary>
        /// Retorna os Endereco paginados.
        /// </summary>
        public async Task<List<CreateEnderecoResponse>> GetAllPagedAsync(int page, int pageSize)
        {
            var endereco = await _repository.GetAllAsync();

            var paged = endereco
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new CreateEnderecoResponse
                {
                    Id = u.Id,
                    Numero = u.Numero,
                    Estado = u.Estado,
                    CodigoPais = u.CodigoPais,
                    CodigoPostal = u.CodigoPostal,
                    Complemento = u.Complemento,
                    Rua = u.Rua
                })
                .ToList();

            return paged;
        }

        public async Task<CreateEnderecoResponse?> GetByIdAsync(long id)
        {
            var endereco = await _repository.GetByIdAsync(id);
            if (endereco == null) return null;

            return new CreateEnderecoResponse
            {
                Id = endereco.Id,
                Numero = endereco.Numero,
                Estado = endereco.Estado,
                CodigoPais = endereco.CodigoPais,
                CodigoPostal = endereco.CodigoPostal,
                Complemento = endereco.Complemento,
                Rua = endereco.Rua
            };
        }

        public async Task<bool> UpdateEnderecoAsync(long id, CreateEnderecoRequest request)
        {
            var endereco = await _repository.GetByIdAsync(id);
            if (endereco == null) return false;

            endereco.Atualizar(
                request.Numero,
                request.Estado,
                request.CodigoPais,
                request.CodigoPostal,
                request.Complemento,
                request.Rua
            );
            _repository.Update(endereco);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteEnderecoAsync(long id)
        {
            var endereco = await _repository.GetByIdAsync(id);
            if (endereco == null) return false;

            _repository.Delete(endereco);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
