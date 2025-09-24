using GB1.Infrastructure.Repositories;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entities;
using OsTresMotoqueirosDoApocalipseVerde.Infraestructure.Context;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.UseCase
{
    public class FuncionarioUseCase
    {
        private readonly IRepository<Funcionario> _funcionarioRepository;
        private readonly IRepository<Dados> _dadosRepository;
        private readonly AppDbContext _context;

        public FuncionarioUseCase(
            IRepository<Funcionario> funcionarioRepository,
            IRepository<Dados> dadosRepository,
            AppDbContext context)
        {
            _funcionarioRepository = funcionarioRepository;
            _dadosRepository = dadosRepository;
            _context = context;
        }

        public async Task<CreateFuncionarioResponse> CreateFuncionarioAsync(CreateFuncionarioRequest request)
        {
            var dados = Dados.Create(
                request.Dados.Nome,
                request.Dados.CPF,
                request.Dados.Telefone,
                request.Dados.Email,
                request.Dados.Senha
            );

            await _dadosRepository.AddAsync(dados);
            await _dadosRepository.SaveChangesAsync();

            var funcionario = Funcionario.Create(request.Cargo, request.FilialId, dados.Id);

            await _funcionarioRepository.AddAsync(funcionario);
            await _funcionarioRepository.SaveChangesAsync();

            return new CreateFuncionarioResponse
            {
                Dados = new CreateDadosResponse
                {
                    Nome = dados.Nome,
                    CPF = dados.CPF,
                    Telefone = dados.Telefone,
                    Email = dados.Email,
                    Senha = dados.Senha,
                },
                Cargo = funcionario.Cargo
                
            };
        }

        public async Task<List<CreateFuncionarioResponse>> GetAllFuncionarioAsync()
        {
            var funcionarios = await _funcionarioRepository.GetAllAsync();
            var response = new List<CreateFuncionarioResponse>();

            foreach (var funcionario in funcionarios)
            {
                var dados = await _dadosRepository.GetByIdAsync(funcionario.DadosId.Value);

                response.Add(new CreateFuncionarioResponse
                {
                    Id = funcionario.Id,
                    Dados = new CreateDadosResponse
                    {
                        Nome = dados.Nome,
                        CPF = dados.CPF,
                        Telefone = dados.Telefone,
                        Email = dados.Email,
                        Senha = dados.Senha,
                    },
                    Cargo = funcionario.Cargo,
                    FilialId = funcionario.FilialId
                });
            }

            return response;
        }

        public async Task<CreateFuncionarioResponse?> GetByIdAsync(long id)
        {
            var funcionario = await _funcionarioRepository.GetByIdAsync(id);
            if (funcionario == null) return null;

            var dados = await _dadosRepository.GetByIdAsync(funcionario.DadosId.Value);

            return new CreateFuncionarioResponse
            {
                Id = funcionario.Id,
                Dados = new CreateDadosResponse
                {
                    Nome = dados.Nome,
                    CPF = dados.CPF,
                    Telefone = dados.Telefone,
                    Email = dados.Email,
                    Senha = dados.Senha,
                },
                Cargo = funcionario.Cargo,
                FilialId = funcionario.FilialId
            };
        }

        public async Task<bool> UpdateFuncionarioAsync(long id, CreateFuncionarioRequest request)
        {
            var funcionario = await _funcionarioRepository.GetByIdAsync(id);
            if (funcionario == null) return false;

            var dados = await _dadosRepository.GetByIdAsync(funcionario.DadosId.Value);
            if (dados == null) return false;

            funcionario.Atualizar(request.Cargo, request.FilialId, dados.Id);
            dados.Atualizar(
                request.Dados.Nome,
                request.Dados.CPF,
                request.Dados.Telefone,
                request.Dados.Email,
                request.Dados.Senha
            );

            _funcionarioRepository.Update(funcionario);
            _dadosRepository.Update(dados);

            await _funcionarioRepository.SaveChangesAsync();
            await _dadosRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteFuncionarioAsync(long id)
        {
            var funcionario = await _funcionarioRepository.GetByIdAsync(id);
            if (funcionario == null) return false;

            var dadosId = funcionario.DadosId;

            _funcionarioRepository.Delete(funcionario);
            await _funcionarioRepository.SaveChangesAsync();

            if (dadosId.HasValue)
            {
                var dados = await _dadosRepository.GetByIdAsync(dadosId.Value);
                if (dados != null)
                {
                    _dadosRepository.Delete(dados);
                    await _dadosRepository.SaveChangesAsync();
                }
            }

            return true;
        }
    }
}
