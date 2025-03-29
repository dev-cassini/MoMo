using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MoMo.Modules.LeadImporter.Domain.Model;

namespace MoMo.Modules.LeadImporter.Infrastructure.EntityFramework;

public class LeadImporterDbContext(DbContextOptions<LeadImporterDbContext> options) : DbContext(options)
{
    public const string Schema = "lead_importer";
    
    public DbSet<Schema> Schemas { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Marker).Assembly);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties().Where(x => x.IsPrimaryKey()))
            {
                property.ValueGenerated = ValueGenerated.Never;
            }
        }
    }
}