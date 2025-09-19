using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entities;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.UseCase
{
    public class EnderecoUseCase
    {
        private readonly IRepository<Endereco> _repository;
        private readonly AppDbContext _context;

        public EnderecoUseCase(IRepository<Endereco> repository, AppDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<CreateEnderecoRequest> CreateEnderecoAsync(CreateEnderecoRequest request)
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


            return endereco.Select(e => new CreateEnderecoResponse
            {
                Id = e.Id,
                Numero = e.Numero,
                Estado = e.Estado,
                CodigoPais = e.CodigoPais,
                CodigoPostal = e.CodigoPostal,
                Complemento = e.Complemento,
                Rua = e.Rua,
            }).ToList();
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
