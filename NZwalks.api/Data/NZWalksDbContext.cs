using Microsoft.EntityFrameworkCore;
using NZwalks.api.Models.Domain;

namespace NZwalks.api.Data
{
    public class NZWalksDbContext:DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext>options):base(options)
        {
            
        }
        public DbSet<Difficulty> difficulties { get; set; }
        public DbSet<Region> regions { get; set; }

        public DbSet<Walk> walks { get; set; }
    }
}
