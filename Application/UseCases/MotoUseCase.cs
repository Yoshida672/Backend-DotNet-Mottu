
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
            var motos = await _context.Motos.ToListAsync();

            return motos.Select(m => new MotoResponse
            {
                Id = m.Id,
                Placa = m.Placa,
                Modelo = m.Modelo,
                Status = m.Status
            });
        }

        public async Task<MotoResponse> GetByIdAsync(Guid id)
        {
            var moto = await _context.Motos.FindAsync(id);
            if (moto == null) return null;

            return new MotoResponse
            {
                Id = moto.Id,
                Placa = moto.Placa,
                Modelo = moto.Modelo,
                Status = moto.Status
            };
        }

        public async Task<MotoResponse> CreateAsync(CreateMoto request)
        {

            var moto = new Moto (
                request.Placa,
                request.Modelo,
                request.Status );

            await _context.Motos.AddAsync(moto);
            await _context.SaveChangesAsync();

            return new MotoResponse
            {
                Id = moto.Id,
                Placa = moto.Placa,
                Modelo = moto.Modelo,
                Status = moto.Status
            };
        }

    }}
