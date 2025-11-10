using Backend_Dotnet_Mottu.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend_Dotnet_Mottu.Infrasctructure.Persistence.Mappings;
internal class CondicaoMapping : IEntityTypeConfiguration<Condicao>
{
    public void Configure(EntityTypeBuilder<Condicao> builder)
    {
        builder.ToTable("condicoes");

        builder.HasKey(c => c.Id);
        builder.Property(m => m.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(c => c.Nome)
                .HasColumnName("nome")
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(c => c.Cor)
                .HasColumnName("cor")
                .IsRequired()
                .HasMaxLength(200);
    }
}

