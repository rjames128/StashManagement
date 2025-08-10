using StashManagement.API.Entities;

namespace StashManagement.API.Infrastructure
{
    public class StashItemRepository : IStashItemRepository
    {
        public Task AddAsync(FabricItem item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<FabricItem>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<FabricItem?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(FabricItem item)
        {
            throw new NotImplementedException();
        }
    }
}
