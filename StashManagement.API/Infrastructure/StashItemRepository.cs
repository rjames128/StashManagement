using Microsoft.EntityFrameworkCore;
using StashManagement.API.DAOs;
using StashManagement.API.Entities;

namespace StashManagement.API.Infrastructure
{
    public class StashItemRepository : IStashItemRepository
    {
        private readonly StashDbContext _context;
        public StashItemRepository(StashDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<int> AddAsync(Guid profileId, FabricItem item)
        {
            _context.FabricItems.Add(item.ToDAO(profileId));
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id, Guid profileId)
        {
            var item = await _context.FabricItems.FirstOrDefaultAsync(i => i.Id == id && i.ProfileId == profileId);
            if(item == null)
            {
                return true;
            }
            _context.Remove(item);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<FabricItem>> GetAllAsync(Guid profileId)
        {
            return await _context.FabricItems
                .Where(i => i.ProfileId == profileId)
                .Select(i => i.ToEntity())
                .ToListAsync();
        }

        public async Task<FabricItem?> GetByIdAsync(Guid id, Guid profileId)
        {
            return await _context.FabricItems
                .Where(i => i.Id == id && i.ProfileId == profileId)
                .Select(i => i.ToEntity())
                .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsync(Guid profileId, FabricItem item)
        {
            var existingItem = await _context.FabricItems.FirstOrDefaultAsync(i => i.Id == item.Id && i.ProfileId == profileId);
            if (existingItem == null)
            {
                return false;
            }
            existingItem = item.ToDAO(profileId);
            _context.Update(existingItem);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
