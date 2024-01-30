using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SamplePipeDelimiter.Models;

namespace SamplePipeDelimiter.Contexts;

public class UlsDbContext : DbContext                                               // This UlsDbContext is the program's gateway to interact with the database
{
    public DbSet<PubAccCO> PubAccCOs { get; set; }

    public UlsDbContext(DbContextOptions<UlsDbContext> options) : base(options)            //added a onstructor that accepts DbContextOptions<UlsDbContext> as an argument
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configurationInstance = new ConfigurationBuilder()
            .SetBasePath(Directory.GetParent(AppContext.BaseDirectory)?.FullName ?? ".")
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile("appsettings.local.json", optional: true)
            .Build();
    string dbConnString = configurationInstance["ConnectionStrings:ulsdb"] ?? "";
    optionsBuilder.UseNpgsql(dbConnString)
                              .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);         // Added this line for logging which means that this will show me what SQL queries are being executed in the background

        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PubAccCO>(co => { co.HasNoKey(); });
    }
}
