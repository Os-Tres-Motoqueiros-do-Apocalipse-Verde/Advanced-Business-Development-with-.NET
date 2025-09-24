using GB1.Infrastructure.Repositories;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entities;
using OsTresMotoqueirosDoApocalipseVerde.Infraestructure.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.UseCase
{
    public class MotoristaUseCase
    {
        private readonly IRepository<Motorista> _motoristaRepository;
        private readonly IRepository<Dados> _dadosRepository;
        private readonly AppDbContext _context;

        public MotoristaUseCase(
            IRepository<Motorista> motoristaRepository,
            IRepository<Dados> dadosRepository,
            AppDbContext context)
        {
            _motoristaRepository = motoristaRepository;
            _dadosRepository = dadosRepository;
            _context = context;
        }

        public async Task<CreateMotoristaResponse> CreateMotoristaAsync(CreateMotoristaRequest request)
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

            var motorista = Motorista.Create(request.Plano, dados.Id);

            await _motoristaRepository.AddAsync(motorista);
            await _motoristaRepository.SaveChangesAsync();

            return new CreateMotoristaResponse
            {
                Dados = new CreateDadosResponse
                {
                    Nome = dados.Nome,
                    CPF = dados.CPF,
                    Telefone = dados.Telefone,
                    Email = dados.Email,
                    Senha = dados.Senha,
                },
                Plano = motorista.Plano

            };
        }

        public async Task<List<CreateMotoristaResponse>> GetAllMotoristaAsync()
        {
            var motoristas = await _motoristaRepository.GetAllAsync();
            var response = new List<CreateMotoristaResponse>();

            foreach (var motorista in motoristas)
            {
                var dados = await _dadosRepository.GetByIdAsync(motorista.DadosId.Value);

                response.Add(new CreateMotoristaResponse
                {
                    Id = motorista.Id,
                    Dados = new CreateDadosResponse
                    {
                        Nome = dados.Nome,
                        CPF = dados.CPF,
                        Telefone = dados.Telefone,
                        Email = dados.Email,
                        Senha = dados.Senha,
                    },
                    Plano = motorista.Plano
                });
            }

            return response;
        }

        public async Task<CreateMotoristaResponse?> GetByIdAsync(long id)
        {
            var motorista = await _motoristaRepository.GetByIdAsync(id);
            if (motorista == null) return null;

            var dados = await _dadosRepository.GetByIdAsync(motorista.DadosId.Value);

            return new CreateMotoristaResponse
            {
                Id = motorista.Id,
                Dados = new CreateDadosResponse
                {
                    Nome = dados.Nome,
                    CPF = dados.CPF,
                    Telefone = dados.Telefone,
                    Email = dados.Email,
                    Senha = dados.Senha,
                },
                Plano = motorista.Plano
            };
        }

        public async Task<bool> UpdateMotoristaAsync(long id, CreateMotoristaRequest request)
        {
            var motorista = await _motoristaRepository.GetByIdAsync(id);
            if (motorista == null) return false;

            var dados = await _dadosRepository.GetByIdAsync(motorista.DadosId.Value);
            if (dados == null) return false;

            dados.Atualizar(
                request.Dados.Nome,
                request.Dados.CPF,
                request.Dados.Telefone,
                request.Dados.Email,
                request.Dados.Senha
            );
            motorista.Atualizar(request.Plano, dados.Id);


            _motoristaRepository.Update(motorista);
            _dadosRepository.Update(dados);

            await _motoristaRepository.SaveChangesAsync();
            await _dadosRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteMotoristaAsync(long id)
        {
            var motorista = await _motoristaRepository.GetByIdAsync(id);
            if (motorista == null) return false;

            var dadosId = motorista.DadosId;

            _motoristaRepository.Delete(motorista);
            await _motoristaRepository.SaveChangesAsync();

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
