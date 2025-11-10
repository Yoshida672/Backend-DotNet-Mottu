using Backend_Dotnet_Mottu.Application.DTOs.Request;
using Backend_Dotnet_Mottu.Application.DTOs.Response;
using Backend_Dotnet_Mottu.Domain.Entities;
using Backend_Dotnet_Mottu.Domain.Pagination;

namespace Backend_Dotnet_Mottu.Application.UseCases
{
    public class MotoUseCase : IUseCase<Moto, CreateMoto, UpdateMotoRequest, MotoResponse>
    {
        private readonly IRepository<Moto> _Repository;
        private readonly IRepository<Condicao> _CondicaoRepository;

        public MotoUseCase(IRepository<Moto> repository,IRepository<Condicao> condicaorepository)
        {
            _Repository = repository;
            _CondicaoRepository = condicaorepository;
        }

        public async Task<MotoResponse> CreateAsync(CreateMoto request)
        {
            var condicao = await _CondicaoRepository.GetByIdAsync(request.CondicaoId);
            if (condicao == null)
                throw new ArgumentException("Condição não encontrada");

            var moto = new Moto(request.Placa, request.Dono, request.Modelo,  condicao, request.PatioId);
            

            await _Repository.AddAsync(moto);

            return MapToResponse(moto);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var moto = await _Repository.GetByIdAsync(id);
            if (moto == null) return false;

            await _Repository.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<MotoResponse>> GetAllAsync()
        {
            var motos = await _Repository.GetAllAsync();
            return motos.Select(MapToResponse);
        }

        public async Task<MotoResponse?> GetByIdAsync(long id)
        {
            var moto = await _Repository.GetByIdAsync(id);
            return moto == null ? null : MapToResponse(moto);
        }

        public async Task<MotoResponse?> UpdateAsync(long id, UpdateMotoRequest request)
        {
            var moto = await _Repository.GetByIdAsync(id);
            if (moto == null) return null;

            var condicao = await _CondicaoRepository.GetByIdAsync(request.CondicaoId);
            if (condicao == null) throw new ArgumentException("Condição não encontrada");
            
            moto.AtualizarDados(request.Placa, request.Dono, request.Modelo, condicao, request.PatioId);


            await _Repository.UpdateAsync(moto);

            return MapToResponse(moto);
        }
        public async Task<PaginatedResult<MotoResponse>> GetPaginatedAsync(int page, int pageSize)
        {
            var (items, totalItems) = await _Repository.GetPaginatedAsync(page, pageSize);

            var responseItems = items.Select(l => new MotoResponse
            {
                Id = l.Id,
                Placa = l.Placa,
                Modelo = l.Modelo,
                Dono = l.Dono,
                CondicaoId = l.CondicaoId,
                PatioId = l.PatioId,
                Condicao = l.Condicao?.Nome,
        
            }).ToList();

            return new PaginatedResult<MotoResponse>
            {
                Items = responseItems,
                TotalItems = totalItems,
                Page = page,
                PageSize = pageSize
            };
        }

        private MotoResponse MapToResponse(Moto moto)
        {
            return new MotoResponse
            {
                Id = moto.Id,
                Placa = moto.Placa,
                Modelo = moto.Modelo,
                Dono = moto.Dono,
                CondicaoId = moto.CondicaoId,
                PatioId = moto.PatioId,
                Condicao = moto.Condicao?.Nome,
            };
        }
    }
}
