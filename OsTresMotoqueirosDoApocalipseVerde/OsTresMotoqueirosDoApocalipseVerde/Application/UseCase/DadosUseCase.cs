using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entity;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.UseCase
{
    public class DadosUseCase
    {
        private readonly IRepository<Dados> _repository;

        public DadosUseCase(IRepository<Dados> repository)
        {
            _repository = repository;
        }

        public async Task<CreateDadosResponse> CreateDadosAsync(CreateDadosRequest request)
        {
            var dados = Dados.Create(
                request.Nome,
                request.CPF,
                request.Telefone,
                request.Email,
                request.Senha
            );

            await _repository.AddAsync(dados);
            await _repository.SaveChangesAsync();

            return new CreateDadosResponse
            {
                Id = dados.Id,
                Nome = dados.Nome,
                CPF = dados.CPF,
                Telefone = dados.Telefone,
                Email = dados.Email,
                Senha = dados.Senha
            };
        }

        public async Task<List<CreateDadosResponse>> GetAllDadosAsync()
        {
            var dados = await _repository.GetAllAsync();
            return dados.Select(u => new CreateDadosResponse
            {
                Id = u.Id,
                Nome = u.Nome,
                CPF = u.CPF,
                Telefone = u.Telefone,
                Email = u.Email,
                Senha = u.Senha
            }).ToList();
        }

        /// <summary>
        /// Retorna os Dados paginados.
        /// </summary>
        public async Task<List<CreateDadosResponse>> GetAllPagedAsync(int page, int pageSize)
        {
            var dados = await _repository.GetAllAsync();

            var paged = dados
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new CreateDadosResponse
                {
                    Id = u.Id,
                    Nome = u.Nome,
                    CPF = u.CPF,
                    Telefone = u.Telefone,
                    Email = u.Email,
                    Senha = u.Senha
                })
                .ToList();

            return paged;
        }

        public async Task<CreateDadosResponse?> GetByIdAsync(long id)
        {
            var dados = await _repository.GetByIdAsync(id);
            if (dados == null) return null;

            return new CreateDadosResponse
            {
                Id = dados.Id,
                Nome = dados.Nome,
                CPF = dados.CPF,
                Telefone = dados.Telefone,
                Email = dados.Email,
                Senha = dados.Senha
            };
        }

        public async Task<bool> UpdateDadosAsync(long id, CreateDadosRequest request)
        {
            var dados = await _repository.GetByIdAsync(id);
            if (dados == null) return false;

            dados.Atualizar(
                request.Nome,
                request.CPF,
                request.Telefone,
                request.Email,
                request.Senha
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
