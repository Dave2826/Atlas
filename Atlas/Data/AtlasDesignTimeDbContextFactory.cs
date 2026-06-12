using Atlas.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Atlas.Data;

public class AtlasDesignTimeDbContextFactory : IDesignTimeDbContextFactory<AtlasDbContext>
{
    public AtlasDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AtlasDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Atlas;Username=postgres;Password=Datos2026");

        return new AtlasDbContext(optionsBuilder.Options);
    }
}