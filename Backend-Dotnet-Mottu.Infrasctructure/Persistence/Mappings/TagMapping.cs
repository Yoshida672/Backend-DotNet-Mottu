
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Backend_Dotnet_Mottu.Domain.Entities;
using MongoDB.Driver;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Backend_Dotnet_Mottu.Infrasctructure.Persistence.Mappings
{
    class TagMapping : IEntityTypeConfiguration<TagUwb>
    {
        public void Configure(EntityTypeBuilder<TagUwb> builder)
        {
            builder.ToTable("uwb");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasColumnName("id")
                .IsRequired();

            builder.Property(t => t.Codigo)
                .HasColumnName("codigo_uwb")
                .HasMaxLength(10)
                .IsRequired();

            var statusConverter = new ValueConverter<bool, string>(
                v => v ? "Ativo" : "Inativo",
                v => v == "Ativo"
            );

            builder.Property(t => t.Status)
                .HasColumnName("status")
                .HasMaxLength(10)
                .HasConversion(statusConverter)
                .IsRequired();

            builder.Property(t => t.MotoId)
                .HasColumnName("id_moto")
                .IsRequired(false);


            builder.HasOne(t => t.Localizacao)
                .WithOne()
                .HasForeignKey<TagUwb>("id_localizacao")
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
