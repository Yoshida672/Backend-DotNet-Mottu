namespace CP2_BackEndMottu_DotNet.Infrastructure.Mapping;
using global::CP2_BackEndMottu_DotNet.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CondicaoMapping : IEntityTypeConfiguration<Condicao>
    {
        public void Configure(EntityTypeBuilder<Condicao> builder)
        {
            builder.ToTable("Condicao");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(c => c.Cor)
                   .HasMaxLength(200);
        }
    } 

