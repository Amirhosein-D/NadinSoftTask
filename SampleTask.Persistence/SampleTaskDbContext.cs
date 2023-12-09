using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SampleTask.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleTask.Persistence
{
    public class SampleTaskDbContext : IdentityDbContext<ApplicationUser>
    {
        public SampleTaskDbContext(DbContextOptions<SampleTaskDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ApplicationUserProduct> ApplicationUserProducts { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUserProduct>()
                .HasKey(sc => new { sc.ProductId, sc.ApplicationUserId });

            modelBuilder.Entity<ApplicationUserProduct>()
                .HasOne(sc => sc.ApplicationUser)
                .WithMany(s => s.ApplicationUserProducts)
                .HasForeignKey(sc => sc.ApplicationUserId);

            modelBuilder.Entity<ApplicationUserProduct>()
                .HasOne(sc => sc.Product)
                .WithMany(c => c.ApplicationUserProducts)
                .HasForeignKey(sc => sc.ProductId);

        }



    }
}
