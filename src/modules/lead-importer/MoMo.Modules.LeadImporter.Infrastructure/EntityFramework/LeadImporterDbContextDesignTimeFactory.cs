using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MoMo.Modules.LeadImporter.Infrastructure.EntityFramework;

public class LeadImporterDbContextDesignTimeFactory : IDesignTimeDbContextFactory<LeadImporterDbContext>
{
    public LeadImporterDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<LeadImporterDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=password;Database=MoMo;Include Error Detail=true");

        return new LeadImporterDbContext(optionsBuilder.Options);
    }
}