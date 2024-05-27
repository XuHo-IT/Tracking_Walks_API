using Microsoft.EntityFrameworkCore;
using NZWalk.API.Data;
using NZWalk.API.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NZWalk.API.Repositories
{
    public class SQLWalksRepository : IWalksRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLWalksRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var existingWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk == null)
            {
                return null;
            }

            dbContext.Walks.Remove(existingWalk);
            await dbContext.SaveChangesAsync();
            return existingWalk;
        }

        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, 
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            var walks = dbContext.Walks.Include(w => w.Difficulty).Include(w => w.Region).AsQueryable();

            // Filtering
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                filterQuery = filterQuery.ToLower();
                switch (filterOn.ToLower())
                {
                    case "name":
                        walks = walks.Where(x => x.Name.ToLower().Contains(filterQuery));
                        break;
                    case "description":
                        walks = walks.Where(x => x.Description.ToLower().Contains(filterQuery));
                        break;
                    case "region":
                        walks = walks.Where(x => x.Region.Name.ToLower().Contains(filterQuery));
                        break;
                    case "difficulty":
                        walks = walks.Where(x => x.Difficulty.Name.ToLower().Contains(filterQuery));
                        break;
                        // Add more cases as needed
                }
            }
            //Sorting
            if (String.IsNullOrWhiteSpace(sortBy) == false) 
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks= isAscending ? walks.OrderBy(x => x.Name) :walks.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("Length",StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInkm) : walks.OrderByDescending (x => x.LengthInkm);
                }
            }
            //Pagination
            var skipResult = (pageNumber - 1) * pageSize;

            return await walks.Skip(skipResult).Take(pageSize).ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await dbContext.Walks
                .Include("Difficulty")
                .Include("Region")
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await dbContext.Walks.FirstOrDefaultAsync(w => w.Id == id);
            if (existingWalk == null)
            {
                return null;
            }

            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.LengthInkm = walk.LengthInkm;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;
            existingWalk.RegionId = walk.RegionId;
            existingWalk.DifficultyId = walk.DifficultyId;

            await dbContext.SaveChangesAsync();
            return existingWalk;
        }
    }
}
