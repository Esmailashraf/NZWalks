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

        public async Task<List<Walk>> GetAllAsync(string? filterOn=null, string? filterQuery=null
            , string? sortBy = null, bool? isAscending = true,
            int pageNumber = 1, int pageSize = 1000)
        {
            var walks = dbContext.walks.Include("Difficulty").Include("Region").AsQueryable();
            //Filter
            if(string.IsNullOrWhiteSpace(filterOn)==false&&string.IsNullOrWhiteSpace(filterQuery)==false)
            {
                if(filterOn.Equals("name",StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }
            //Sorting 
            if(string.IsNullOrWhiteSpace(sortBy)==false)
            {
                var ascending = isAscending ?? true;
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = ascending ? walks.OrderBy(u => u.Name) : walks.OrderByDescending(u => u.Name);
                }
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = ascending ? walks.OrderBy(u => u.LengthInKm) : walks.OrderByDescending(u => u.LengthInKm);
                }
            }
            //Pagenation
            var skipResult = (pageNumber - 1) * pageSize;
            return await walks.Skip(skipResult).Take(pageSize).ToListAsync();
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
