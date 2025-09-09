    using CP2_BackEndMottu_DotNet.Application.DTOs.Request;
    using CP2_BackEndMottu_DotNet.Application.DTOs.Response;
    using CP2_BackEndMottu_DotNet.Domain.Entity;
    using CP2_BackEndMottu_DotNet.Domain.Interface;
    using CP2_BackEndMottu_DotNet.Infrastructure.Persistence.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CondicaoUseCase : IUseCase<Condicao, CreateCondicaoRequest, UpdateCondicaoRequest, CondicaoResponse>
    {
        private readonly IRepository<Condicao> _repository;

        public CondicaoUseCase(IRepository<Condicao> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CondicaoResponse>> GetAllAsync()
        {
            var condicoes = await _repository.GetAllAsync();
            return condicoes.Select(c => new CondicaoResponse
            {
                Id = c.Id,
                Nome = c.Nome,
                Cor = c.Cor
            });
        }

        public async Task<CondicaoResponse?> GetByIdAsync(Guid id)
        {
            var condicao = await _repository.GetByIdAsync(id);
            if (condicao == null) return null;

            return new CondicaoResponse
            {
                Id = condicao.Id,
                Nome = condicao.Nome,
                Cor = condicao.Cor
            };
        }

        public async Task<CondicaoResponse> CreateAsync(CreateCondicaoRequest request)
        {
            var nova = new Condicao(request.Nome, request.Cor);
            await _repository.AddAsync(nova);

            return new CondicaoResponse
            {
                Id = nova.Id,
                Nome = nova.Nome,
                Cor = nova.Cor
            };
        }

        public async Task<CondicaoResponse?> UpdateAsync(Guid id, UpdateCondicaoRequest request)
        {
            var condicao = await _repository.GetByIdAsync(id);
            if (condicao == null) return null;

            condicao.Nome = request.Nome;
            condicao.Cor = request.Cor;

            await _repository.UpdateAsync(condicao);

            return new CondicaoResponse
            {
                Id = condicao.Id,
                Nome = condicao.Nome,
                Cor = condicao.Cor
            };
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var condicao = await _repository.GetByIdAsync(id);
            if (condicao == null) return false;

            await _repository.DeleteAsync(condicao);
            return true;
        }
    }
