using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PokiMani.Infrastructure.Data;
using System;

namespace PokiMani.Infrastructure.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // Read the connection string from an environment variable
            var connStr = Environment.GetEnvironmentVariable("POKIMANI_DB");
            if (string.IsNullOrEmpty(connStr))
            {
                throw new InvalidOperationException(
                    "Environment variable 'POKIMANI_DB' not set. " +
                    "Set it to your PostgreSQL connection string before running migrations.");
            }

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql(connStr);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}