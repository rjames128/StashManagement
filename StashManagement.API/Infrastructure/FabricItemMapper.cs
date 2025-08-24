using StashManagement.API.Entities;
using StashManagement.API.DAOs;

namespace StashManagement.API.Infrastructure
{
    public static class FabricItemMapper
    {
        public static FabricItemDAO ToDAO(this FabricItem item, Guid profileId)
        {
            return new FabricItemDAO
            {
                Id = item.Id,
                ProfileId = profileId,
                Name = item.Name,
                Description = item.Description,
                SourceLocation = item.SourceLocation,
                CreatedAt = item.CreatedAt,
                UpdatedAt = item.UpdatedAt,
                Cut = item.Cut,
                Amount = item.Amount
            };
        }

        public static FabricItem ToEntity(this FabricItemDAO dao, string url)
        {
            return new FabricItem
            {
                Id = dao.Id,
                Name = dao.Name,
                Description = dao.Description,
                SourceLocation = dao.SourceLocation,
                CreatedAt = dao.CreatedAt,
                UpdatedAt = dao.UpdatedAt,
                Cut = dao.Cut,
                Amount = dao.Amount,
                ImageSrc = url
            };
        }
    }
}