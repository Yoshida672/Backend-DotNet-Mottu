using CP2_BackEndMottu_DotNet.Domain.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CP2_BackEndMottu_DotNet.Infrastructure.Mapping
{
    public class MotoMapping : IEntityTypeConfiguration<Moto>
    {
        public void Configure(EntityTypeBuilder<Moto> builder)
        {
            builder.ToTable("Moto");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Placa)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(m => m.Modelo)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(m => m.Status)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasMany(m => m.Localizacoes)
                   .WithOne(l => l.Moto)
                   .HasForeignKey(l => l.MotoId);
        }
    }
}