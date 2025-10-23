using Microsoft.EntityFrameworkCore;
using PokiMani.Models.Entities;

namespace PokiMani.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected ApplicationDbContext()
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Bucket> Buckets { get; set; }

        public DbSet<Account> Accounts { get; set; }
    }
}
