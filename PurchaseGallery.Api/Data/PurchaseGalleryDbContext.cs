using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PurchaseGallery.Api.Models.Assets;
using PurchaseGallery.Api.Models.Auth;
using PurchaseGallery.Api.Models.Comments;
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
        public required DbSet<PurchaseRequestStatus> PurchaseRequestsStauts { get; set; }
        public required DbSet<Approval> Approvals { get; set; }
        public required DbSet<Notifications> Notifications { get; set; }
        public required DbSet<Role> Roles { get; set; }
        public required DbSet<Traveller> Travellers { get; set; }
        public required DbSet<Country> Countries { get; set; }
        public required DbSet<Comment> Comments { get; set; }
        public required DbSet<PurchaseRequestStatus> PurchaseRequestStatuses { get; set; }



        /// <summary>
        /// Configures the model that was discovered by convention from the entity types
        /// exposed in <see cref="DbSet{TEntity}"/> properties on your derived context. The resulting
        /// model may be cached and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRoles",
                    j => j
                        .HasOne<Role>()
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade),
                    j => j
                        .HasOne<User>()
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("UserRoles");
                    });



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

            modelBuilder.Entity<Notifications>()
                .HasOne(n => n.PurchaseRequest)
                .WithMany()
                .HasForeignKey(n => n.PurchaseRequestId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PurchaseRequest>()
                .HasOne(pr => pr.ApproverIds)
                .WithMany()
                .HasForeignKey(pr => pr.Approvers)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PurchaseRequest>()
                .HasMany(pr => pr.Assets)
                .WithMany()
                .UsingEntity(j => j.ToTable("PurchaseRequestAssets"));

            modelBuilder.Entity<PurchaseRequest>()
                .HasOne(pr => pr.RequesterUser)
                .WithMany()
                .HasForeignKey(pr => pr.RequsterId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PurchaseRequest>()
                .HasMany(pr => pr.Comments)
                .WithOne()
                .HasForeignKey(c => c.PurchaseRequestId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PurchaseRequest>()
                .HasOne(pr => pr.Status)
                .WithMany()
                .HasForeignKey(pr => pr.StatusId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Commenter)
                .WithMany()
                .HasForeignKey(c => c.CommenterId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.ParentComment)
                .WithMany(c => c.Replies)
                .HasForeignKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);


            //     modelBuilder.Entity<Asset>();
            //     /* -------------------------------------------------------------------------- */
            //     /*                              // User and Role                              */
            //     /* -------------------------------------------------------------------------- */
            //     modelBuilder.Entity<User>()
            //         .HasMany(u => u.Roles)
            //         .WithMany(r => r.Users)
            //         .UsingEntity(j => j.ToTable("UserRoles"));

            //     /* -------------------------------------------------------------------------- */
            //     /*                                  // Assets                                 */
            //     /* -------------------------------------------------------------------------- */
            //     modelBuilder.Entity<Asset>()
            //         .HasOne(a => a.AssetCategory)
            //         .WithMany()
            //         .HasForeignKey(a => a.AssetCategoryId)
            //         .OnDelete(DeleteBehavior.Cascade);

            //     modelBuilder.Entity<Asset>()
            //         .HasOne(a => a.AssetType)
            //         .WithMany()
            //         .HasForeignKey(a => a.AssetTypeId)
            //         .OnDelete(DeleteBehavior.Cascade);


            //     /* -------------------------------------------------------------------------- */
            //     /*                                // Travellers                               */
            //     /* -------------------------------------------------------------------------- */
            //     modelBuilder.Entity<Traveller>()
            //    .HasOne(t => t.FromWhere)
            //    .WithMany()
            //    .HasForeignKey(t => t.FromWhereId)
            //    .OnDelete(DeleteBehavior.NoAction);

            //     modelBuilder.Entity<Traveller>()
            //         .HasOne(t => t.ToWhere)
            //         .WithMany()
            //         .HasForeignKey(t => t.ToWhereId)
            //         .OnDelete(DeleteBehavior.NoAction);
            //     /* -------------------------------------------------------------------------- */
            //     /*                                // Approvals                                */
            //     /* -------------------------------------------------------------------------- */
            //     modelBuilder.Entity<Approval>()
            //         .HasOne(a => a.Approver)
            //         .WithMany()
            //         .HasForeignKey(a => a.ApproverId)
            //         .OnDelete(DeleteBehavior.NoAction);

            //     modelBuilder.Entity<Approval>()
            //         .HasOne(a => a.PurchaseRequest)
            //         .WithMany()
            //         .HasForeignKey(a => a.PurchaseRequestId)
            //         .OnDelete(DeleteBehavior.NoAction);

            //     /* -------------------------------------------------------------------------- */
            //     /*                              // Notifications                              */
            //     /* -------------------------------------------------------------------------- */
            //     modelBuilder.Entity<Notifications>()
            //         .HasOne(n => n.PurchaseRequest)
            //         .WithMany()
            //         .HasForeignKey(n => n.PurchaseRequestId)
            //         .OnDelete(DeleteBehavior.NoAction);

            //     /* -------------------------------------------------------------------------- */
            //     /*                             // Purchase Request                             */
            //     /* -------------------------------------------------------------------------- */
            //     modelBuilder.Entity<PurchaseRequest>()
            //         .HasOne(pr => pr.ApprovedBy)
            //         .WithMany()
            //         .HasForeignKey(pr => pr.ApproverId).OnDelete(DeleteBehavior.NoAction);


            //     modelBuilder.Entity<PurchaseRequest>()
            //         .HasMany(pr => pr.Assets)
            //         .WithMany()  // No need for a navigation property in Asset; it's only defined on PurchaseRequest
            //         .UsingEntity(j => j.ToTable("PurchaseRequestAssets")); // Optional: defines a join table if needed

            //     modelBuilder.Entity<PurchaseRequest>()
            //         .HasOne(pr => pr.RequesterUser)
            //         .WithMany()
            //         .HasForeignKey(pr => pr.RequsterId)
            //         .OnDelete(DeleteBehavior.NoAction);

            //     modelBuilder.Entity<PurchaseRequest>()
            //         .HasMany(pr => pr.Comments)
            //         .WithOne()
            //         .HasForeignKey(c => c.PurchaseRequestId)
            //         .OnDelete(DeleteBehavior.Cascade);

            //     modelBuilder.Entity<PurchaseRequest>()
            //         .HasOne(pr => pr.Status)
            //         .WithMany()
            //         .HasForeignKey(pr => pr.StatusId)
            //         .OnDelete(DeleteBehavior.NoAction);



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