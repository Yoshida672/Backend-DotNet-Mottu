using Backend_Dotnet_Mottu.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend_Dotnet_Mottu.Infrasctructure.Persistence.Mappings
{
    public class LocalizacaoMapping : IEntityTypeConfiguration<LocalizacaoUWB>
    {
        public void Configure(EntityTypeBuilder<LocalizacaoUWB> builder)
        {
            builder.ToTable("localizacao");

            builder.HasKey(t => t.Id);

            builder.Property(m => m.Id)
            .HasColumnName("id")
            .IsRequired();


            builder.Property(t => t.CoordenadaX)
                    .HasColumnName("coordenada_x")
                   .IsRequired();
            builder.Property(t => t.CoordenadaY)
                    .HasColumnName("coordenada_y")
                    .IsRequired();
            builder.Property(t => t.DataHora)
                    .HasColumnName("timestamp")
                   .IsRequired();

        }
    }
}
