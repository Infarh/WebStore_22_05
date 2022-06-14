using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.Entities.Orders;

namespace WebStore.DAL.Context;

public class WebStoreDB : IdentityDbContext<User, Role, string>
{
    public DbSet<Product> Products { get; set; } = null!;

    public DbSet<Brand> Brands { get; set; } = null!;

    public DbSet<Section> Sections { get; set; } = null!;

    public DbSet<Employee> Employees { get; set; } = null!;

    public DbSet<Order> Orders { get; set; } = null!;

    //public DbSet<OrderItem> OrderItems { get; set; } = null!;

    public WebStoreDB(DbContextOptions<WebStoreDB> options) : base(options)
    {
        
    }

    //protected override void OnModelCreating(ModelBuilder db)
    //{
    //    base.OnModelCreating(db);

    //    //db.Entity<Section>()
    //    //   .HasMany(s => s.Products)
    //    //   .WithOne(p => p.Section)
    //    //   .OnDelete(DeleteBehavior.Cascade);
    //}
}
