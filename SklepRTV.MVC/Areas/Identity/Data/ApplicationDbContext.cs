using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SklepRTV.Model;
namespace SklepRTV.MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>,
   IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>
       options)
        : base(options)
        {
        }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<JobPosition> JobPositions { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Worker> Workers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>().OwnsOne(a => a.addressDetails);
            modelBuilder.Entity<Worker>().OwnsOne(a => a.addressDetails);
            modelBuilder.Entity<Customer>().OwnsOne(c => c.contactDetails);
            modelBuilder.Entity<Worker>().OwnsOne(c => c.contactDetails);
            modelBuilder.Entity<Order>().OwnsOne(o => o.CustomerAddress);
        }
    }
}