using Microsoft.EntityFrameworkCore;
using SklepRTV.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SklepRTV.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            
        }

        public DbSet<Branch> Branches { get; set; }
        public DbSet<Category> Categories {  get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<JobPosition> JobPositions { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product>  Products { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Worker> Workers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().OwnsOne(a => a.addressDetails);
            modelBuilder.Entity<Worker>().OwnsOne(a => a.addressDetails);

            modelBuilder.Entity<Customer>().OwnsOne(c => c.contactDetails);
            modelBuilder.Entity<Worker>().OwnsOne(c => c.contactDetails);
        }

    }
}
