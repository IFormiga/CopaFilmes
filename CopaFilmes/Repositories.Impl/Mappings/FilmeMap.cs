using Microsoft.EntityFrameworkCore;
using CopaFilmes.Entities.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CopaFilmes.Domain.Repositories.Impl.Mappings
{
    public class FilmeMap : IEntityTypeConfiguration<Filme>
    {
        public void Configure(EntityTypeBuilder<Filme> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Titulo).IsRequired();
            builder.Property(t => t.Nota).IsRequired();
            builder.Property(t => t.Ano).IsRequired();
        }
    }
}
