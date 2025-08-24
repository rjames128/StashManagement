using Microsoft.EntityFrameworkCore;
using StashManagement.API.Entities;
using StashManagement.API.DAOs;

namespace StashManagement.API.Infrastructure
{
    public class StashDbContext : DbContext
    {
        public StashDbContext(DbContextOptions<StashDbContext> options)
            : base(options)
        {
        }

        public DbSet<FabricItemDAO> FabricItems { get; set; }
        public DbSet<StashItemDAO> StashItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StashItemDAO>(entity =>
            {
                entity.ToTable("stash_item");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.ProfileId).HasColumnName("profile_id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.SourceLocation).HasColumnName("source_location");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
                entity.Property(e => e.ImageKey).HasColumnName("image_key");
                entity.Property(e => e.Bucket).HasColumnName("bucket");
            });

            modelBuilder.Entity<FabricItemDAO>(entity =>
            {
                entity.ToTable("fabric_item");
                //entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Cut).HasColumnName("cut");
                entity.Property(e => e.Amount).HasColumnName("amount");
                entity.HasOne<StashItemDAO>()
                      .WithOne()
                      .HasForeignKey<FabricItemDAO>(e => e.Id)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}