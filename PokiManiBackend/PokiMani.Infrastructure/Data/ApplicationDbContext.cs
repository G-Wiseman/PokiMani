using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PokiMani.Core.Entities;

namespace PokiMani.Infrastructure.Data
{

    public class ApplicationDbContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
    {


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Optional parameterless constructor for design-time tools
        protected ApplicationDbContext() { }

        // Your application-specific tables
        public DbSet<Envelope> Envelopes { get; set; }
        public DbSet<Account> Accounts { get; set; }

        public DbSet<AccountTransaction> AccountTransactions { get; set; }

        public DbSet<EnvelopeTransaction> EnvelopeTransactions { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}