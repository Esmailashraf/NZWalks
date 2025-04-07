using Microsoft.EntityFrameworkCore;
using NZwalks.api.Data;
using NZwalks.api.Models.Domain;

namespace NZwalks.api.Repositories
{
    public class SqlWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SqlWalkRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();

            return walk;

        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var existing = await dbContext.walks.FirstOrDefaultAsync(u => u.Id == id);
            if (existing == null)
            {
                return null;
            }
            dbContext.walks.Remove(existing);
            await dbContext.SaveChangesAsync();

            return existing;
        }

        public async Task<List<Walk>> GetAllAsync()
        {
            return await dbContext.walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await dbContext.walks.Include("Difficulty")
                .Include("Region")
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var existing = await dbContext.walks.FirstOrDefaultAsync(u => u.Id == id);
            if(existing==null)
            {
                return null;
            }
            existing.Name = walk.Name;
            existing.Description = walk.Description;
            existing.LengthInKm = walk.LengthInKm;
            existing.WalkImageUrl = walk.WalkImageUrl;
            existing.DifficultyId = walk.DifficultyId;
            existing.RegionId = walk.RegionId;

            await dbContext.SaveChangesAsync();
            return existing;

        }
    }
}
