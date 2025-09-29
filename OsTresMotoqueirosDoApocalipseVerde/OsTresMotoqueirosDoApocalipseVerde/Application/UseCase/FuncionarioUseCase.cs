﻿using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entity;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.UseCase
{
    public class FuncionarioUseCase
    {
        private readonly IRepository<Funcionario> _repository;

        public FuncionarioUseCase(IRepository<Funcionario> repository)
        {
            _repository = repository;
        }

        public async Task<CreateFuncionarioResponse> CreateFuncionarioAsync(CreateFuncionarioRequest request)
        {
            var funcionario = Funcionario.Create(
                request.Cargo,
                request.DadosId,
                request.FilialId
            );

            await _repository.AddAsync(funcionario);
            await _repository.SaveChangesAsync();

            return new CreateFuncionarioResponse
            {
                Id = funcionario.Id,
                Cargo = funcionario.Cargo,
                DadosId = funcionario.DadosId,
                FilialId = funcionario.FilialId
            };
        }

        public async Task<List<CreateFuncionarioResponse>> GetAllFuncionarioAsync()
        {
            var funcionario = await _repository.GetAllAsync();
            return funcionario.Select(u => new CreateFuncionarioResponse
            {
                Id = u.Id,
                Cargo = u.Cargo,
                DadosId = u.DadosId,
                FilialId = u.FilialId
            }).ToList();
        }

        /// <summary>
        /// Retorna os Funcionario paginados.
        /// </summary>
        public async Task<List<CreateFuncionarioResponse>> GetAllPagedAsync(int page, int pageSize)
        {
            var funcionario = await _repository.GetAllAsync();

            var paged = funcionario
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new CreateFuncionarioResponse
                {
                    Id = u.Id,
                    Cargo = u.Cargo,
                    DadosId = u.DadosId,
                    FilialId = u.FilialId
                })
                .ToList();

            return paged;
        }

        public async Task<CreateFuncionarioResponse?> GetByIdAsync(long id)
        {
            var funcionario = await _repository.GetByIdAsync(id);
            if (funcionario == null) return null;

            return new CreateFuncionarioResponse
            {
                Id = funcionario.Id,
                Cargo = funcionario.Cargo,
                DadosId = funcionario.DadosId,
                FilialId = funcionario.FilialId
            };
        }

        public async Task<bool> UpdateFuncionarioAsync(long id, CreateFuncionarioRequest request)
        {
            var funcionario = await _repository.GetByIdAsync(id);
            if (funcionario == null) return false;

            funcionario.Atualizar(
                request.Cargo,
                request.DadosId,
                request.FilialId
            );
            _repository.Update(funcionario);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteFuncionarioAsync(long id)
        {
            var funcionario = await _repository.GetByIdAsync(id);
            if (funcionario == null) return false;

            _repository.Delete(funcionario);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
