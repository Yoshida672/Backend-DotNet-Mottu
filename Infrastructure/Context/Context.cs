using CP2_BackEndMottu_DotNet.Domain.Entity;
using CP2_BackEndMottu_DotNet.Domain.Interface;
using CP2_BackEndMottu_DotNet.Infrastructure.Mapping;
using Microsoft.EntityFrameworkCore;

namespace CP2_BackEndMottu_DotNet.Infrastructure.Context
{
    public class Context : DbContext, IContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Moto> Motos { get; set; }
        public DbSet<LocalizacaoUWB> Localizacoes { get; set; }
        public DbSet<Condicao> Condicoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MotoMapping());
            modelBuilder.ApplyConfiguration(new LocalizacaoMapping());
            modelBuilder.ApplyConfiguration(new CondicaoMapping());
        }
    }
}