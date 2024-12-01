using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PurchaseGallery.Api.Models.Assets;
using PurchaseGallery.Api.Models.Auth;
using PurchaseGallery.Api.Models.Countries;
using PurchaseGallery.Api.Models.Notifications;
using PurchaseGallery.Api.Models.PurchaseRequests;
using PurchaseGallery.Api.Models.travellers;
using PurchaseGallery.ApiService.Models;
using PurchaseGallery.ApiService.Models.Auth;

namespace PurchaseGallery.ApiService.Data
{
    public partial class PurchaseGalleryDbContext(DbContextOptions<PurchaseGalleryDbContext> options, IConfiguration config) : DbContext(options)
    {
        public IConfiguration _config { get; set; } = config;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("DatabaseConnection"));

        }
        public required DbSet<User> Users { get; set; }
        public required DbSet<Asset> Asset { get; set; }
        public required DbSet<AssetCategory> AssetCategory { get; set; }
        public required DbSet<AssetType> AssetType { get; set; }
        public required DbSet<PurchaseRequest> PurchaseRequests { get; set; }
        public required DbSet<Approval> Approvals { get; set; }
        public required DbSet<Notifications> Notifications { get; set; }
        public required DbSet<Role> Roles { get; set; }
        public required DbSet<UserRole> UserRoles { get; set; }
        public required DbSet<Traveller> Travellers { get; set; }
        public required DbSet<Country> Countries { get; set; }



        /// <summary>
        /// Configures the model that was discovered by convention from the entity types
        /// exposed in <see cref="DbSet{TEntity}"/> properties on your derived context. The resulting
        /// model may be cached and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Asset>();
            /* -------------------------------------------------------------------------- */
            /*                              // User and Role                              */
            /* -------------------------------------------------------------------------- */
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            /* -------------------------------------------------------------------------- */
            /*                                  // Assets                                 */
            /* -------------------------------------------------------------------------- */
            modelBuilder.Entity<Asset>()
                           .HasOne(a => a.AssetCategory)
                           .WithMany()
                           .HasForeignKey(a => a.AssetCategoryId)
                           .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Asset>()
                .HasOne(a => a.AssetType)
                .WithMany()
                .HasForeignKey(a => a.AssetTypeId)
                .OnDelete(DeleteBehavior.Cascade);


            /* -------------------------------------------------------------------------- */
            /*                                // Travellers                               */
            /* -------------------------------------------------------------------------- */
            modelBuilder.Entity<Traveller>()
           .HasOne(t => t.FromWhere)
           .WithMany()
           .HasForeignKey(t => t.FromWhereId)
           .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Traveller>()
                .HasOne(t => t.ToWhere)
                .WithMany()
                .HasForeignKey(t => t.ToWhereId)
                .OnDelete(DeleteBehavior.NoAction);
            /* -------------------------------------------------------------------------- */
            /*                                // Approvals                                */
            /* -------------------------------------------------------------------------- */
            modelBuilder.Entity<Approval>()
                .HasOne(a => a.Approver)
                .WithMany()
                .HasForeignKey(a => a.ApproverId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Approval>()
                .HasOne(a => a.PurchaseRequest)
                .WithMany()
                .HasForeignKey(a => a.PurchaseRequestId)
                .OnDelete(DeleteBehavior.NoAction);

            /* -------------------------------------------------------------------------- */
            /*                              // Notifications                              */
            /* -------------------------------------------------------------------------- */
            modelBuilder.Entity<Notifications>()
                .HasOne(n => n.PurchaseRequest)
                .WithMany()
                .HasForeignKey(n => n.PurchaseRequestId)
                .OnDelete(DeleteBehavior.NoAction);

            /* -------------------------------------------------------------------------- */
            /*                             // PurchaseRequest                             */
            /* -------------------------------------------------------------------------- */
            modelBuilder.Entity<PurchaseRequest>()
                .HasOne(pr => pr.ApprovedBy)
                .WithMany()
                .HasForeignKey(pr => pr.ApproverId).OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<PurchaseRequest>()
            .HasMany(pr => pr.Assets)
        .WithMany()  // No need for a navigation property in Asset; it's only defined on PurchaseRequest
        .UsingEntity(j => j.ToTable("PurchaseRequestAssets")); // Optional: defines a join table if needed

            modelBuilder.Entity<PurchaseRequest>()
                    .HasOne(pr => pr.RequesterUser)
                    .WithMany()
                    .HasForeignKey(pr => pr.RequsterId)
                    .OnDelete(DeleteBehavior.NoAction);
            // modelBuilder.Entity<PurchaseRequest>()
            //     .HasOne(pr => pr.Assets)
            //     .WithMany()
            //     .HasForeignKey(pr => pr.AssetIds)
            //     .OnDelete(DeleteBehavior.Cascade);
            // modelBuilder.Entity<PurchaseRequest>()
            //     .HasMany(pr => pr.Assets)
            //     .WithOne()
            //     .HasForeignKey(a => a.Id)
            //     .OnDelete(DeleteBehavior.Cascade);

            // modelBuilder.Entity<Traveller>()
            //     .HasOne(t => t.FromWhere)
            //     .WithMany()
            //     .HasForeignKey(t => t.FromWhereId)
            //     .OnDelete(DeleteBehavior.Restrict);
            // modelBuilder.Entity<Traveller>()
            //     .HasOne(t => t.FromWhere)
            //     .WithMany()
            //     .HasForeignKey(t => t.FromWhereId)
            //     .OnDelete(DeleteBehavior.Restrict);

            // modelBuilder.Entity<Traveller>()
            //     .HasOne(t => t.ToWhere)
            //     .WithMany()
            //     .HasForeignKey(t => t.ToWhereId)
            //     .OnDelete(DeleteBehavior.Restrict);
        }
    }
}