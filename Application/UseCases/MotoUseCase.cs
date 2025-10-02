using CP2_BackEndMottu_DotNet.Application.DTOs.Request;
using CP2_BackEndMottu_DotNet.Application.DTOs.Response;
using CP2_BackEndMottu_DotNet.Application.UseCases.impl;
using CP2_BackEndMottu_DotNet.Domain.Entity;
using CP2_BackEndMottu_DotNet.Domain.Enum;
using CP2_BackEndMottu_DotNet.Domain.Pagination;
using CP2_BackEndMottu_DotNet.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CP2_BackEndMottu_DotNet.Application.UseCases
{
    public class MotoUseCase : IUseCase<Moto, CreateMoto, UpdateMotoRequest, MotoResponse>
    {
        private readonly Context _context;

        public MotoUseCase(Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MotoResponse>> GetAllAsync()
        {
            var motos = await _context.Motos
                .Include(m => m.Condicao)
                .AsNoTracking()
                .ToListAsync();

            return motos.Select(m => new MotoResponse
            {
                Id = m.Id,
                Placa = m.Placa,
                Modelo = m.Modelo.ToString(),
                Status = m.Status,
                CondicaoId = m.CondicaoId
            });
        }

        public async Task<MotoResponse?> GetByIdAsync(Guid id)
        {
            var moto = await _context.Motos
                .Include(m => m.Condicao)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (moto == null) return null;

            return new MotoResponse
            {
                Id = moto.Id,
                Placa = moto.Placa,
                Modelo = moto.Modelo.ToString(),
                Status = moto.Status,
                CondicaoId = moto.CondicaoId
            };
        }

        public async Task<MotoResponse> CreateAsync(CreateMoto request)
        {
            // Validação do Modelo
            if (!Enum.TryParse<Modelo>(request.Modelo, out var modeloParsed))
                throw new ArgumentException("Modelo inválido");

            // Validação da Condição
            var condicao = await _context.Condicoes.FindAsync(request.CondicaoId);
            if (condicao == null)
                throw new ArgumentException("Condição não encontrada");

            var moto = new Moto(request.Placa, modeloParsed, request.Status, condicao);

            await _context.Motos.AddAsync(moto);
            await _context.SaveChangesAsync();

            return new MotoResponse
            {
                Id = moto.Id,
                Placa = moto.Placa,
                Modelo = moto.Modelo.ToString(),
                Status = moto.Status,
                CondicaoId = moto.CondicaoId
            };
        }

        public async Task<MotoResponse?> UpdateAsync(Guid id, UpdateMotoRequest request)
        {
            var moto = await _context.Motos
                .Include(m => m.Condicao)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (moto == null) return null;

            if (!Enum.TryParse<Modelo>(request.Modelo, out var modeloParsed))
                throw new ArgumentException("Modelo inválido");

            var condicao = await _context.Condicoes.FindAsync(request.CondicaoId);
            if (condicao == null)
                throw new ArgumentException("Condição não encontrada");

            moto.AtualizarDados(request.Placa, modeloParsed, request.Status, condicao);

            _context.Motos.Update(moto);
            await _context.SaveChangesAsync();

            return new MotoResponse
            {
                Id = moto.Id,
                Placa = moto.Placa,
                Modelo = moto.Modelo.ToString(),
                Status = moto.Status,
                CondicaoId = moto.CondicaoId
            };
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var moto = await _context.Motos.FindAsync(id);
            if (moto == null) return false;

            _context.Motos.Remove(moto);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<PaginatedResult<MotoResponse>> GetPaginatedAsync(int page, int pageSize)
        {
            var skip = (page - 1) * pageSize;

            var query = _context.Motos
                .Include(m => m.Condicao)
                .AsNoTracking();

            var totalItems = await query.CountAsync();

            var items = await query
                .OrderBy(m => m.Placa)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            var responseItems = items.Select(m => new MotoResponse
            {
                Id = m.Id,
                Placa = m.Placa,
                Modelo = m.Modelo.ToString(),
                Status = m.Status,
                CondicaoId = m.CondicaoId
            }).ToList();

            return new PaginatedResult<MotoResponse>
            {
                Items = responseItems,
                TotalItems = totalItems,
                Page = page,
                PageSize = pageSize
            };
        }
    }
}
