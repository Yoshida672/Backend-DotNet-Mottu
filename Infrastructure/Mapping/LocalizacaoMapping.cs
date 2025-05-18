using CP2_BackEndMottu_DotNet.Domain.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CP2_BackEndMottu_DotNet.Infrastructure.Mapping
{
    public class LocalizacaoMapping : IEntityTypeConfiguration<LocalizacaoUWB>
    {
        public void Configure(EntityTypeBuilder<LocalizacaoUWB> builder)
        {
            builder.ToTable("Localizacoes");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Id)
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();

            builder.Property(l => l.MotoId)
                .IsRequired()
                .HasColumnName("MOTO_ID");

            builder.Property(l => l.CoordenadaX)
                .IsRequired()
                .HasColumnType("NUMBER(10,6)") 
                .HasColumnName("LATITUDE");

            builder.Property(l => l.CoordenadaY)
                .IsRequired()
                .HasColumnType("NUMBER(10,6)")
                .HasColumnName("LONGITUDE");

            builder.Property(l => l.DataHora)
                .IsRequired()
                .HasColumnType("TIMESTAMP")
                .HasColumnName("TIMESTAMP");

            builder.HasOne(l => l.Moto)
                .WithMany(m => m.Localizacoes)
                .HasForeignKey(l => l.MotoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
