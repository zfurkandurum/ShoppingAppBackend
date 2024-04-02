using Microsoft.EntityFrameworkCore;
using ShoppingAppBackend.Models;

namespace ShoppingAppBackend.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }

    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserDetail> UserDetails { get; set; }

    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cart>().HasKey(c => c.CartId);
        modelBuilder.Entity<CartItem>().HasKey(ci => ci.CartItemId);
        modelBuilder.Entity<Category>().HasKey(cat => cat.CategoryId);
        modelBuilder.Entity<Order>().HasKey(o => o.OrderId);
        modelBuilder.Entity<OrderItem>().HasKey(oi => oi.OrderItemId);
        modelBuilder.Entity<Product>().HasKey(p => p.ProductId);
        modelBuilder.Entity<User>().HasKey(u => u.UserId);
        modelBuilder.Entity<UserDetail>().HasKey(ud => ud.UserDetailId);
        
        //----------------Product
        modelBuilder.Entity<Product>()
            .HasOne(p => p.category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);

        modelBuilder.Entity<Product>()
            .HasMany(p => p.Items)
            .WithOne(ci => ci.Product)
            .HasForeignKey(p => p.CartItemId);
        //----------------User
        modelBuilder.Entity<User>()
            .HasOne(u => u.Cart)
            .WithOne(c => c.User)
            .HasForeignKey<Cart>(c => c.CartId);
        
        modelBuilder.Entity<User>()
            .HasOne(u => u.Detail)
            .WithOne(ud => ud.User)
            .HasForeignKey<User>(u => u.UserDetailId);

        modelBuilder.Entity<User>()
            .HasMany<Order>(u => u.Orders)
            .WithOne(o => o.User)
            .HasForeignKey(u => u.OrderId);
        //----------------Order
        modelBuilder.Entity<Order>()
            .HasMany(o => o.OrderItems)
            .WithOne(oi => oi.Order)
            .HasForeignKey(o => o.OrderItemId);
        //----------------Cart
        modelBuilder.Entity<Cart>()
            .HasMany(c => c.Items)
            .WithOne(ci => ci.Cart)
            .HasForeignKey(c => c.CartItemId);
    }
}
