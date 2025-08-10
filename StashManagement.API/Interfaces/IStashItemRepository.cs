using StashManagement.API.Entities;

namespace StashManagement.API.Interfaces
{
    public interface IStashItemRepository
    {
        public Task<IEnumerable<FabricItem>> GetAllAsync();
        public Task<FabricItem?> GetByIdAsync(Guid id);
        public Task AddAsync(FabricItem item);
        public Task<bool> UpdateAsync(FabricItem item);
        public Task<bool> DeleteAsync(Guid id);
    }
}
