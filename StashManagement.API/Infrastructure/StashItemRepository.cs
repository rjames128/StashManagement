using StashManagement.API.Entities;

namespace StashManagement.API.Infrastructure
{
    public class StashItemRepository : IStashItemRepository
    {
        public Task AddAsync(Guid profileId, FabricItem item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id, Guid profileId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<FabricItem>> GetAllAsync(Guid profileId)
        {
            throw new NotImplementedException();
        }

        public Task<FabricItem?> GetByIdAsync(Guid id, Guid profileId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Guid profileId, FabricItem item)
        {
            throw new NotImplementedException();
        }
    }
}
