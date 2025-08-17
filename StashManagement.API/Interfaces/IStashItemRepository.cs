using StashManagement.API.Entities;

namespace StashManagement.API.Interfaces
{
    public interface IStashItemRepository
    {
        public Task<IEnumerable<FabricItem>> GetAllAsync(Guid profileId);
        public Task<FabricItem?> GetByIdAsync(Guid id, Guid profileId);
        public Task<int> AddAsync(Guid profileId, FabricItem item);
        public Task<bool> UpdateAsync(Guid profileId, FabricItem item);
        public Task<bool> DeleteAsync(Guid id, Guid profileId);
    }
}
