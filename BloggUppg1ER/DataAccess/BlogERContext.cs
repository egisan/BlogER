using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BloggUppg1ER.Models
{
    public partial class BlogERContext : DbContext
    {
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Posts> Posts { get; set; }

        // I create the Constructor for the newly scaffolded DB Context
        public BlogERContext(DbContextOptions<BlogERContext> options)
             : base(options)
        { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<Posts>(entity =>
            {
                entity.HasKey(e => e.PostId);

                entity.Property(e => e.Content).HasMaxLength(2000);

                entity.Property(e => e.PostedOn).HasColumnType("date");

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_dbo.Posts_dbo.Categories_CategoryId");
            });
        }
    }
}
