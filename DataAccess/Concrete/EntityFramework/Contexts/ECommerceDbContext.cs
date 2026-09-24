namespace DataAccess.Concrete.EntityFramework.Contexts;

using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

public class ECommerceDbContext : DbContext
{
    public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
    {
    }

    public ECommerceDbContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=e_commerce_db;Username=postgres;Password=1234;Client Encoding=UTF8;");
        }
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<OperationClaim> OperationClaims => Set<OperationClaim>();
    public DbSet<UserOperationClaim> UserOperationClaims => Set<UserOperationClaim>();
    public DbSet<Address> Addresses => Set<Address>();
    public DbSet<Store> Stores => Set<Store>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Brand> Brands => Set<Brand>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductVariant> ProductVariants => Set<ProductVariant>();
    public DbSet<ProductImage> ProductImages => Set<ProductImage>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<SubOrder> SubOrders => Set<SubOrder>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<Cart> Carts => Set<Cart>();
    public DbSet<CartItem> CartItems => Set<CartItem>();
    public DbSet<Favorite> Favorites => Set<Favorite>();
    public DbSet<ProductReview> ProductReviews => Set<ProductReview>();
    public DbSet<ProductQuestion> ProductQuestions => Set<ProductQuestion>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ECommerceDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}