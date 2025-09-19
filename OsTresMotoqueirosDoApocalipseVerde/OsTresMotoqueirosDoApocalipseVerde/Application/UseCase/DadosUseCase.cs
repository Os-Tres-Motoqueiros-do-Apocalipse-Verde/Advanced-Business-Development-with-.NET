using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response;
using OsTresMotoqueirosDoApocalipseVerde.Infraestructure.Context;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.UseCase
{
    public class DadosUseCase
    {
        private readonly IRepository<Dados> _repository;
        private readonly AppDbContext _context;

        public DadosUseCase(IRepository<Dados> repository, AppDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<CreateDadosRequest> CreateDadosAsync(CreateDadosRequest request)
        {
            var dados = Dados.Create(
                request.CPF,
                request.Telefone,
                request.Email,
                request.Senha,
                request.Nome
            );

            await _repository.AddAsync(dados);
            await _repository.SaveChangesAsync();

            return new CreateDadosResponse
            {
                Id = dados.Id,
                CPF = dados.CPF,
                Telefone = dados.Telefone,
                Email = dados.Email,
                Senha = dados.Senha,
                Nome = dados.Nome
            };
        }

        public async Task<List<CreateDadosResponse>> GetAllDadosAsync()
        {
            var dados = await _repository.GetAllAsync();


            return dados.Select(d => new CreateDadosResponse
            {
                Id = d.Id,
                CPF = d.CPF,
                Telefone = d.Telefone,
                Email = d.Email,
                Senha = d.Senha,
                Nome = d.Nome,
            }).ToList();
        }

        public async Task<CreateDadosResponse?> GetByIdAsync(long id)
        {
            var dados = await _repository.GetByIdAsync(id);
            if (dados == null) return null;

            return new CreateDadosResponse
            {
                Id = dados.Id,
                CPF = dados.CPF,
                Telefone = dados.Telefone,
                Email = dados.Email,
                Senha = dados.Senha,
                Nome = dados.Nome
            };
        }


        public async Task<bool> UpdateDadosAsync(long id, CreateDadosRequest request)
        {
            var dados = await _repository.GetByIdAsync(id);
            if (dados == null) return false;

            dados.Atualizar(
                request.CPF,
                request.Telefone,
                request.Email,
                request.Senha,
                request.Nome
            );

            _repository.Update(dados);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteDadosAsync(long id)
        {
            var dados = await _repository.GetByIdAsync(id);
            if (dados == null) return false;

            _repository.Delete(dados);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
