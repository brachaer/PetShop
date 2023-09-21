using Microsoft.EntityFrameworkCore;
using PetShop.Model.Entities;

namespace PetShop.Data
{
    public partial class PetShopDbContext : DbContext
    {
        public PetShopDbContext()
        {
        }

        public PetShopDbContext(DbContextOptions<PetShopDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Animal> Animals { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>(entity =>
            {
                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Animals)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Animal__Category__398D8EEE");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasOne(d => d.Animal)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.AnimalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Comment__AnimalI__3C69FB99");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
