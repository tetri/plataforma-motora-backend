using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PlataformaMotora.Domain.Entities;

namespace PlataformaMotora.Infrastructure.Mappings
{
    public class VeiculoMap : IEntityTypeConfiguration<Veiculo>
    {
        public void Configure(EntityTypeBuilder<Veiculo> builder)
        {
            builder.ToTable("Veiculos");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Placa)
                .IsRequired()
                .HasMaxLength(7);

            builder.Property(v => v.Marca)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(v => v.Modelo)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(v => v.AnoFabricacao)
                .IsRequired();

            builder.Property(v => v.AnoModelo)
                .IsRequired();

            builder.Property(v => v.CriadoEm).IsRequired();
            builder.Property(v => v.CriadoPor);
            builder.Property(v => v.ModificadoEm);
            builder.Property(v => v.ModificadoPor);
            builder.Property(v => v.Excluido).HasDefaultValue(false);
            builder.Property(v => v.ExcluidoEm);
            builder.Property(v => v.ExcluidoPor);
        }
    }
}
