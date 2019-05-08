using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Rehber.Model.DataModels;

namespace Rehber.Data.DbContexts
{
    public partial class RehberEmployeeServiceDbContext : DbContext
    {
        public RehberEmployeeServiceDbContext()
        {
        }

        public RehberEmployeeServiceDbContext(DbContextOptions<RehberEmployeeServiceDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employees> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=RehberEmployeeService;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.Property(e => e.Email).HasMaxLength(250);

                entity.Property(e => e.ExtraInfo).HasMaxLength(250);

                entity.Property(e => e.FirstName).HasMaxLength(250);

                entity.Property(e => e.LastName).HasMaxLength(250);

                entity.Property(e => e.TelephoneNumber).HasMaxLength(250);

                entity.Property(e => e.WebSite).HasMaxLength(250);
            });
        }
    }
}