using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entities;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;
using OsTresMotoqueirosDoApocalipseVerde.Infraestructure.Context;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.UseCase
{
    public class FuncionarioUseCase
    {
        private readonly IRepository<Funcionario> _repository;
        private readonly AppDbContext _context;

        public FuncionarioUseCase(IRepository<Funcionario> repository, AppDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        public FuncionarioUseCase(IRepository<Funcionario> repository, IRepository<Dados> dadosRepository, AppDbContext context)
        {
            _repository = repository;
            _dadosRepository = dadosRepository;
            _context = context;
        }

        public async Task<FuncionarioResponse> CreateFuncionarioAsync(CreateFuncionarioRequest request)
        {
            if (request == null || request.Dados == null)

            // Criar os Dados do Funcionario
            var dados = Dados.Create(
                request.Dados.Nome,
                request.Dados.Email,
                request.Dados.Telefone
            );

            await _dadosRepository.AddAsync(dados);
            await _dadosRepository.SaveChangesAsync();

            var funcionario = Funcionario.Create(
                request.Cargo,
                dados.Id,
                request.FilialId 
            );

            await _repository.AddAsync(funcionario);
            await _repository.SaveChangesAsync();

            return new CreateFuncionarioResponse
            {
                Id = funcionario.Id,
                DadosId = funcionario.DadosId,
                Cargo = funcionario.Cargo,
                FilialId = funcionario.FilialId
                
            };
        }

        // Exemplo de um método para obter o Funcionario por ID
        public async Task<FuncionarioResponse?> GetFuncionarioByIdAsync(long id)
        {
            var funcionario = await _context.Funcionarios
                .Include(f => f.Dados)   // Incluindo os Dados relacionados ao Funcionario
                .Include(f => f.Filial)  // Incluindo a Filial se necessário
                .FirstOrDefaultAsync(f => f.Id == id);

            if (funcionario == null) return null;

            return new FuncionarioResponse
            {
                Id = funcionario.Id,
                Cargo = funcionario.Cargo,
                FilialId = funcionario.FilialId,
                DadosId = funcionario.DadosId,
                Dados = new DadosResponse
                {
                    Nome = funcionario.Dados.Nome,
                    Email = funcionario.Dados.Email,
                    Telefone = funcionario.Dados.Telefone
                }
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
