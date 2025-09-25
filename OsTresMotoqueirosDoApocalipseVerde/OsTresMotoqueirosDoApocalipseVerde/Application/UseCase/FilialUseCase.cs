using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entities;
using OsTresMotoqueirosDoApocalipseVerde.Infraestructure.Context;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.UseCase
{
    public class FilialUseCase
    {
        private readonly IRepository<Filial> _filialRepository;
        private readonly IRepository<Endereco> _enderecoRepository;
        private readonly AppDbContext _context;

        public FilialUseCase(
            IRepository<Filial> filialRepository,
            IRepository<Endereco> enderecoRepository,
            AppDbContext context)
        {
            _filialRepository = filialRepository;
            _enderecoRepository = enderecoRepository;
            _context = context;
        }

        public async Task<CreateFilialResponse> CreateFilialAsync(CreateFilialRequest request)
        {
            var endereco = Endereco.Create(
                request.Endereco.Numero,
                request.Endereco.Estado,
                request.Endereco.CodigoPais,
                request.Endereco.CodigoPostal,
                request.Endereco.Complemento,
                request.Endereco.Rua
            );

            await _enderecoRepository.AddAsync(endereco);
            await _enderecoRepository.SaveChangesAsync();

            var filial = Filial.Create(request.NomeFilial, endereco.Id);

            await _filialRepository.AddAsync(filial);
            await _filialRepository.SaveChangesAsync();

            return new CreateFilialResponse
            {
                NomeFilial = filial.NomeFilial,
                Endereco = new CreateEnderecoResponse
                {
                    Numero = endereco.Numero,
                    Estado = endereco.Estado,
                    CodigoPais = endereco.CodigoPais,
                    CodigoPostal = endereco.CodigoPostal,
                    Complemento = endereco.Complemento,
                    Rua = endereco.Rua
                }
            };
        }

        public async Task<List<CreateFilialResponse>> GetAllFiliaisAsync()
        {
            var filiais = await _filialRepository.GetAllAsync();
            var response = new List<CreateFilialResponse>();

            foreach (var filial in filiais)
            {
                var endereco = await _enderecoRepository.GetByIdAsync(filial.EnderecoId);

                response.Add(new CreateFilialResponse
                {
                    Id = filial.Id,
                    NomeFilial = filial.NomeFilial,
                    Endereco = new CreateEnderecoResponse
                    {
                        Numero = endereco.Numero,
                        Estado = endereco.Estado,
                        CodigoPais = endereco.CodigoPais,
                        CodigoPostal = endereco.CodigoPostal,
                        Complemento = endereco.Complemento,
                        Rua = endereco.Rua
                    }
                });
            }

            return response;
        }

        public async Task<CreateFilialResponse?> GetByIdAsync(long id)
        {
            var filial = await _filialRepository.GetByIdAsync(id);
            if (filial == null) return null;

            var endereco = await _enderecoRepository.GetByIdAsync(filial.EnderecoId);

            return new CreateFilialResponse
            {
                Id = filial.Id,
                NomeFilial = filial.NomeFilial,
                Endereco = new CreateEnderecoResponse
                {
                    Numero = endereco.Numero,
                    Estado = endereco.Estado,
                    CodigoPais = endereco.CodigoPais,
                    CodigoPostal = endereco.CodigoPostal,
                    Complemento = endereco.Complemento,
                    Rua = endereco.Rua
                }
            };
        }

        public async Task<bool> UpdateFilialAsync(long id, CreateFilialRequest request)
        {
            var filial = await _filialRepository.GetByIdAsync(id);
            if (filial == null) return false;

            var endereco = await _enderecoRepository.GetByIdAsync(filial.EnderecoId);
            if (endereco == null) return false;

            filial.Atualizar(request.NomeFilial, filial.EnderecoId);
            endereco.Atualizar(
                request.Endereco.Numero,
                request.Endereco.Estado,
                request.Endereco.CodigoPais,
                request.Endereco.CodigoPostal,
                request.Endereco.Complemento,
                request.Endereco.Rua
            );

            _filialRepository.Update(filial);
            _enderecoRepository.Update(endereco);

            await _filialRepository.SaveChangesAsync();
            await _enderecoRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteFilialAsync(long id)
        {
            var filial = await _filialRepository.GetByIdAsync(id);
            if (filial == null) return false;

            var enderecoId = filial.EnderecoId;

            _filialRepository.Delete(filial);
            await _filialRepository.SaveChangesAsync();

            var endereco = await _enderecoRepository.GetByIdAsync(enderecoId);
            if (endereco != null)
            {
                _enderecoRepository.Delete(endereco);
                await _enderecoRepository.SaveChangesAsync();
            }

            return true;
        }

    }
}
