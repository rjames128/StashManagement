using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.EntityFrameworkCore;
using StashManagement.API.Entities;
using StashManagement.API.Configuration;

namespace StashManagement.API.Infrastructure
{
    public class StashItemRepository : IStashItemRepository
    {
        private readonly StashDbContext _context;
        private readonly IAmazonS3 _s3Client;
        private readonly AppSettings _settings;
        public StashItemRepository(StashDbContext context, IAmazonS3 s3Client, AppSettings settings)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _s3Client = s3Client;
            _settings = settings;
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
                .Select(i => i.ToEntity(_s3Client.GetPreSignedURL(new GetPreSignedUrlRequest
                {
                    BucketName = i.Bucket,
                    Key = i.ImageKey,
                    Expires = DateTime.UtcNow.AddMinutes(15)
                })))
                .ToListAsync();
        }

        public async Task<FabricItem?> GetByIdAsync(Guid id, Guid profileId)
        {
            var item =  await _context.FabricItems
                .Where(i => i.Id == id && i.ProfileId == profileId).FirstOrDefaultAsync();

            return item.ToEntity(await _s3Client.GetPreSignedURLAsync(new GetPreSignedUrlRequest
            {
                BucketName = item?.Bucket,
                Key = item?.ImageKey,
                Expires = DateTime.UtcNow.AddMinutes(15)
            }));
        }

        public async Task<bool> UpdateAsync(Guid profileId, FabricItem item)
        {
            var existingItem = await _context.FabricItems.FirstOrDefaultAsync(i => i.Id == item.Id && i.ProfileId == profileId);
            if (existingItem == null)
            {
                return false;
            }
            var updates = item.ToDAO(profileId);
            existingItem.SourceLocation = updates.SourceLocation;
            existingItem.Amount = updates.Amount;
            existingItem.Cut = updates.Cut;
            existingItem.Description = updates.Description;
            existingItem.Name = updates.Name;
            existingItem.UpdatedAt = updates.UpdatedAt;
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> UploadImageAsync(Guid id, Guid profileId, IFormFile image)
        {
            var item = await _context.FabricItems.FirstOrDefaultAsync(i => i.Id == id && i.ProfileId == profileId);
            if (item == null) return false;
            var bucket = _settings.AWS.StashItemBucketName;
            var key = $"fabric/{Guid.NewGuid()}_{image.FileName}";
            using var stream = image.OpenReadStream();
            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = stream,
                Key = key,
                BucketName = bucket,
                ContentType = image.ContentType
            };
            var transferUtility = new TransferUtility(_s3Client);
            await transferUtility.UploadAsync(uploadRequest);
            item.Bucket = bucket;
            item.ImageKey = key;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
