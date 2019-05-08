using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Rehber.Model.DataModels;

namespace Rehber.Data.DBContexts
{
    public partial class RehberImageServisContext : DbContext
    {
        public RehberImageServisContext()
        {
        }

        public RehberImageServisContext(DbContextOptions<RehberImageServisContext> options)
            : base(options)
        {
        }

        public virtual DbSet<UserImages> UserImages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=RehberImageServis;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserImages>(entity =>
            {
                entity.HasKey(e => e.FotId);

                entity.Property(e => e.FotId).HasColumnName("fotId");

                entity.Property(e => e.BinaryData)
                    .HasColumnName("binaryData")
                    .HasMaxLength(8000);

                entity.Property(e => e.EmployeeId).HasColumnName("employeeId");

                entity.Property(e => e.EmployeeName)
                    .HasColumnName("employeeName")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });
        }
    }
}
