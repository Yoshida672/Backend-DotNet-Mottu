using Backend_Dotnet_Mottu.Domain.Entities;
using Backend_Dotnet_Mottu.Domain.ValueObjects;
using Backend_Dotnet_Mottu.Infrasctructure.Persistence.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Backend_Dotnet_Mottu.Infrasctructure.Persistence.Context
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Moto> Motos { get; set; }
        public DbSet<LocalizacaoUWB> Localizacoes { get; set; }
        public DbSet<Condicao> Condicoes { get; set; }
        public DbSet<TagUwb> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MotoMapping());
            modelBuilder.ApplyConfiguration(new LocalizacaoMapping());
            modelBuilder.ApplyConfiguration(new CondicaoMapping());
            modelBuilder.ApplyConfiguration(new TagMapping());
        }
    }
}
