using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SamplePipeDelimiter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplePipeDelimiter.Contexts
{
    public class UlsDbContext : DbContext
    {
        public DbSet<PubAccCO> PubAccCOs { get; set; }

        public UlsDbContext()
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
            string dbConnString = configurationInstance["ConnectionStrings:UlsDb"] ?? "";
            Console.WriteLine($"Connection String: {dbConnString}");
            optionsBuilder.UseNpgsql(dbConnString);
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PubAccCO>(co => { co.HasNoKey(); });
        }
    }
}
