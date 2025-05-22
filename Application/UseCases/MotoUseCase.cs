using CP2_BackEndMottu_DotNet.Application.DTOs.Request;
using CP2_BackEndMottu_DotNet.Application.DTOs.Response;
using CP2_BackEndMottu_DotNet.Domain.Entity;
using CP2_BackEndMottu_DotNet.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CP2_BackEndMottu_DotNet.Application.UseCases
{
    public class MotoUseCase
    {
        private readonly MotoContext _context;

        public MotoUseCase(MotoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MotoResponse>> GetAllAsync()
        {
            var motos = await _context.Motos
                .Include(m => m.Condicao)
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

        public async Task<MotoResponse> GetByIdAsync(Guid id)
        {
            var moto = await _context.Motos
                .Include(m => m.Condicao)
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
            var modeloValido = Enum.TryParse<Modelo>(request.Modelo, out var modeloParsed);
            if (!modeloValido)
                throw new ArgumentException("Modelo inválido");
            var condicao = await _context.Condicoes.FindAsync(request.CondicaoId);
            if (condicao == null)
                throw new ArgumentException("Condição não encontrada");
            var moto = new Moto(
                request.Placa,
                modeloParsed,
                request.Status ,
                condicao
                );

            await _context.Motos.AddAsync(moto);
            await _context.SaveChangesAsync();

            return new MotoResponse
            {
                Id = moto.Id,
                Placa = moto.Placa,
                Modelo = moto.Modelo.ToString(),
                Status = moto.Status,
                CondicaoId = moto.CondicaoId,
            };
        }
    }
}
