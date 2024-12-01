using Microsoft.EntityFrameworkCore;
using PurchaseGallery.ApiService.Models;

namespace PurchaseGallery.ApiService.Data
{
    public partial class PurchaseGalleryDbContext : DbContext
    {
        public IConfiguration _config { get; set; }
        public PurchaseGalleryDbContext(IConfiguration config)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("DatabaseConnection"));

        }

        public DbSet<Users> Users { get; set; }



        //public virtual DbSet<Users> Users { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Users>(entity =>
        //    {
        //        entity.HasKey(k => k.Id);
        //    });
        //    OnModelCreatingPartial(modelBuilder);

        //    //base.OnModelCreating(modelBuilder);
        //}

        //protected void OnModelCreatingPartial(ModelBuilder modelBuilder)
        //{
        //}
    }
}