using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManagerApi.Models
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
           
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseSqlServer("Database:FileManger")
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(b =>
            {
                b.HasIndex(u => u.Username).IsUnique();
                //b.Property(u => u.Role).HasDefaultValue(Role.User);
            });

            modelBuilder.Entity<UserRole>(b =>
            {
                b.HasOne(u => u.User).WithMany(ux => ux.Roles);
                b.HasKey(u => new { u.UserId, u.Role});
            });
        }

    }
}
