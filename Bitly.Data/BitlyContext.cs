using Bitly.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Bitly.Data
{
    public class BitlyContext : DbContext
    {
        public BitlyContext() : base("BitlyContext")
        {
            
        }

        public DbSet<Link> Links { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
