using CP2_BackEndMottu_DotNet.Application.DTOs.Request;
using CP2_BackEndMottu_DotNet.Application.DTOs.Response;
using CP2_BackEndMottu_DotNet.Domain.Entity;
using CP2_BackEndMottu_DotNet.Infrastructure.Persistence.Repositories;

namespace CP2_BackEndMottu_DotNet.Application.UseCases
{
    public class CondicaoUseCase
    {
        private readonly IRepository<Condicao> _repository;

        public CondicaoUseCase(IRepository<Condicao> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CondicaoResponse>> GetAllAsync()
        {
            var condicoes = await _repository.GetAllAsync();
            return condicoes.Select(x => new CondicaoResponse
            {
                Id = x.Id,
                Nome = x.Nome,
                Cor = x.Cor
            });
        }

        public async Task<CondicaoResponse?> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;

            return new CondicaoResponse
            {
                Id = entity.Id,
                Nome = entity.Nome,
                Cor = entity.Cor
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
        public async Task<Condicao?> UpdateAsync(Guid id, UpdateCondicaoRequest request)
        {
            var condicao = await _repository.GetByIdAsync(id);
            if (condicao == null)
                return null;

            condicao.Nome = request.Nome;
            condicao.Cor = request.Cor;

            await _repository.UpdateAsync(condicao);
            return condicao;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var condicao = await _repository.GetByIdAsync(id);
            if (condicao == null)
                return false;

            await _repository.DeleteAsync(condicao);
            return true;
        }
    }
}
