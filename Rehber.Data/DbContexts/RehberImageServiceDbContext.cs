using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Rehber.Model.DataModels;

namespace Rehber.Data.Contexts
{
    public partial class RehberImageServiceDbContext : DbContext
    {
        public RehberImageServiceDbContext()
        {
        }

        public RehberImageServiceDbContext(DbContextOptions<RehberImageServiceDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<UserImages> UserImages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=RehberImageService;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserImages>(entity =>
            {
                entity.HasKey(e => e.ImageId);

                entity.Property(e => e.ImageBinaryData)
                    .IsRequired()
                    .HasMaxLength(8000);
            });
        }
    }
}
