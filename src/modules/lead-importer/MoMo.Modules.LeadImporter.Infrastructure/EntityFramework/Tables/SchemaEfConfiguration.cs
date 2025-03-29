using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoMo.Modules.LeadImporter.Domain.Model;

namespace MoMo.Modules.LeadImporter.Infrastructure.EntityFramework.Tables;

public class SchemaEfConfiguration : IEntityTypeConfiguration<Schema>
{
    public void Configure(EntityTypeBuilder<Schema> builder)
    {
        builder.ToTable(nameof(LeadImporterDbContext.Schemas), LeadImporterDbContext.Schema);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.CreationTimestampUtc);
        builder.Property(p => p.JsonSchema)
            .HasColumnType("jsonb")
            .IsRequired();
    }
}