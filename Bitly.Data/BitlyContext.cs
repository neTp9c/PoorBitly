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

        public virtual IDbSet<Link> Links { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }


        
        private void FixEfProviderServicesProblem()
        {
            // EntityFramework.SqlServer.dll will not automatically copy on project building to web project bin folder without it.
            // http://stackoverflow.com/a/19130718/1315751

            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
    }
}
