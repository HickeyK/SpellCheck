using SpellCheck.Entities;
using System.Data.Entity;

namespace SpellCheck
{
    public class SpellCheckContext : DbContext 
    {
        public DbSet<Spelling> Spelling { get; set; }
        public DbSet<SpellTest> SpellTest { get; set; }
    }
}
