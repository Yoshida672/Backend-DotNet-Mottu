using Backend_Dotnet_Mottu.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend_Dotnet_Mottu.Infrasctructure.Persistence.Mappings;

public class MotoMapping : IEntityTypeConfiguration<Moto>
{
    public void Configure(EntityTypeBuilder<Moto> builder)
    {
        builder.ToTable("moto");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(m => m.Placa)
            .HasColumnName("placa")
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(m => m.Modelo)
            .HasColumnName("modelo")
            .HasConversion<string>()
            .IsRequired();



        builder.Property(m => m.Dono)
            .HasColumnName("dono_moto")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(m => m.CondicaoId)
            .HasColumnName("id_condicao")
            .IsRequired();

        builder.Property(m => m.PatioId)
            .HasColumnName("id_patio")
            .IsRequired();

        builder.HasOne(m => m.TagUwb)
       .WithOne(t => t.Moto)
       .HasForeignKey<TagUwb>(t => t.MotoId)
       .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(m => m.Condicao)
                      .WithOne(c => c.Moto)
                      .HasForeignKey<Moto>(m => m.CondicaoId)
                      .OnDelete(DeleteBehavior.Restrict);

    }
}


