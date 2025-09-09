using CP2_BackEndMottu_DotNet.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

            builder.OwnsOne(l => l.Coordenada, c =>
            {
                c.Property(p => p.X)
                    .HasColumnName("LATITUDE")
                    .HasColumnType("NUMBER(10,6)")
                    .IsRequired();

                c.Property(p => p.Y)
                    .HasColumnName("LONGITUDE")
                    .HasColumnType("NUMBER(10,6)")
                    .IsRequired();
            });

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
