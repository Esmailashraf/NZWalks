using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NZwalks.api.Data;
using NZwalks.api.Models.Domain;

namespace NZwalks.api.Repositories
{
    public class SqlRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;
        public SqlRegionRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;

        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existing = await dbContext.regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null)
            {
                return null;
            }
            dbContext.regions.Remove(existing);
            await dbContext.SaveChangesAsync();
            return existing;
        }

        

        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await dbContext.regions.FirstOrDefaultAsync(x => x.Id == id);
            

        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existing = await dbContext.regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null)
            {
                return null;
            }
            existing.Code = region.Code;
            existing.Name = region.Name;
            existing.RegionImageUrl = region.RegionImageUrl;

            await dbContext.SaveChangesAsync();
            return existing;
        }
    }
}
