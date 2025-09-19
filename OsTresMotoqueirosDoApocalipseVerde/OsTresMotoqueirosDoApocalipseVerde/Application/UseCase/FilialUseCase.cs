using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entities;
using OsTresMotoqueirosDoApocalipseVerde.Infraestructure.Context;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.UseCase
{
    public class FilialUseCase
    {
        private readonly IRepository<Filial> _repository;
        private readonly AppDbContext _context;

        public FilialUseCase(IRepository<Filial> repository, AppDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<CreateFilialRequest> CreateFilialAsync(CreateFilialRequest request)
        {
            var filial = Filial.Create(
                request.NomeFilial,
                request.EnderecoId
            );

            await _repository.AddAsync(filial);
            await _repository.SaveChangesAsync();

            return new CreateFilialResponse
            {
                Id = filial.Id,
                Numero = filial.NomeFilial
            };
        }

        public async Task<List<CreateFilialResponse>> GetAllFilialAsync()
        {
            var filial = await _repository.GetAllAsync();


            return filial.Select(f => new CreateFilialResponse
            {
                Id = f.Id,
                Numero = f.NomeFilial,
                Estado = f.EstadoId
            }).ToList();
        }

        public async Task<CreateFilialResponse?> GetByIdAsync(long id)
        {
            var filial = await _context.Filial
                .Include(f => f.Endereco)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (filial == null) return null;

            return new CreateFilialResponse
            {
                Id = filial.Id,
                NomeFilial = filial.NomeFilial,
                Endereco = filial.Endereco == null ? null : new CreateEnderecoResponse
                {
                    Id = filial.Endereco.Id,
                    Numero = filial.Endereco.Numero,
                    Estado = filial.Endereco.Estado,
                    CodigoPais = filial.Endereco.CodigoPais,
                    CodigoPostal = filial.Endereco.CodigoPostal,
                    Complemento = filial.Endereco.Complemento,
                    Rua = filial.Endereco.Rua
                }
            };
        }


        public async Task<bool> UpdateFilialAsync(long id, CreateFilialRequest request)
        {
            var filial = await _repository.GetByIdAsync(id);
            if (filial == null) return false;

            filial.Atualizar(
                request.NomeFilial,
                request.EnderecoId
            );

            _repository.Update(filial);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteFilialAsync(long id)
        {
            var filial = await _repository.GetByIdAsync(id);
            if (filial == null) return false;

            _repository.Delete(filial);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
