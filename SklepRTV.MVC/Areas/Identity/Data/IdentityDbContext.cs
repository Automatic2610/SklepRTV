﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SklepRTV.Model;
using SklepRTV.MVC.Areas.Identity.Data;

namespace SklepRTV.MVC.Data;

public class IdentityDbContext : IdentityDbContext<SklepRTV.MVC.Areas.Identity.Data.User>
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
        : base(options)
    {
    }

    public DbSet<Branch> Branches { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<JobPosition> JobPositions { get; set; }
    public DbSet<Order> Orders { get; set; }
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

       modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
    }
}

public class ApplicationUserConfiguration : IEntityTypeConfiguration<SklepRTV.MVC.Areas.Identity.Data.User>
{
    public void Configure(EntityTypeBuilder<Areas.Identity.Data.User> builder)
    {
        
        builder.Property(x => x.FirstName).HasMaxLength(50);
        builder.Property(x => x.LastName).HasMaxLength(50);
    }
}