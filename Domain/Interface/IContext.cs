using CP2_BackEndMottu_DotNet.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace CP2_BackEndMottu_DotNet.Domain.Interface
{
    public interface IContext
    {
        DbSet<Moto> Motos { get; }
        DbSet<LocalizacaoUWB> Localizacoes { get; }
        DbSet<Condicao> Condicoes { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
