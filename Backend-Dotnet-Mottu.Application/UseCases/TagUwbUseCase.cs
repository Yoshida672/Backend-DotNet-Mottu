
using Backend_Dotnet_Mottu.Application.DTOs.Request;
using Backend_Dotnet_Mottu.Application.DTOs.Response;
using Backend_Dotnet_Mottu.Domain.Entities;
using Backend_Dotnet_Mottu.Domain.Pagination;

namespace Backend_Dotnet_Mottu.Application.UseCases
{
    public class TagUwbUseCase : IUseCase<TagUwb, CreateTagUwb, UpdateTagUwbRequest, TagUwbResponse>
    {
        private readonly IRepository<TagUwb> _repository;

        public TagUwbUseCase(IRepository<TagUwb> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TagUwbResponse>> GetAllAsync()
        {
            var tags = await _repository.GetAllAsync();
            return tags.Select(ToResponse);
        }

        public async Task<TagUwbResponse?> GetByIdAsync(long id)
        {
            var tag = await _repository.GetByIdAsync(id);
            return tag == null ? null : ToResponse(tag);
        }

        public async Task<TagUwbResponse> CreateAsync(CreateTagUwb request)
        {
            var tag = new TagUwb(request.Codigo, request.Status, request.MotoId ?? 0);
            await _repository.AddAsync(tag);
            return ToResponse(tag);
        }

        public async Task<TagUwbResponse?> UpdateAsync(long id, UpdateTagUwbRequest request)
        {
            var tag = await _repository.GetByIdAsync(id);
            if (tag == null) return null;

            tag.AtualizarDados(request.Codigo, request.Status, request.MotoId ?? 0);
            await _repository.UpdateAsync(tag);

            return ToResponse(tag);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var tag = await _repository.GetByIdAsync(id);
            if (tag == null) return false;

            await _repository.DeleteAsync(id);
            return true;
        }

        public async Task<PaginatedResult<TagUwbResponse>> GetPaginatedAsync(int page, int pageSize)
        {
            var result = await _repository.GetPaginatedAsync(page, pageSize);

            return new PaginatedResult<TagUwbResponse>
            {
                Items = result.Items.Select(ToResponse).ToList(),
                TotalItems = result.TotalItems,
                Page = page,
                PageSize = pageSize
            };
        }

        private static TagUwbResponse ToResponse(TagUwb tag)
        {
            return new TagUwbResponse
            {
                Id = tag.Id,
                Codigo = tag.Codigo,
                Status = tag.Status ? "Ativo" : "Inativo",
                MotoId = tag.MotoId == 0 ? null : tag.MotoId,
                Localizacao = tag.Localizacao == null
                    ? null
                    : new LocalizacaoResponse
                    {
                        CoordenadaX = tag.Localizacao.CoordenadaX,
                        CoordenadaY = tag.Localizacao.CoordenadaY,
                        DataHora = tag.Localizacao.DataHora
                    }
            };
        }
    }
}
