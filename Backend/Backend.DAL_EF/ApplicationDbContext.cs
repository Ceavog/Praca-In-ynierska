using Backend.DataAccessLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Options;
using Pomelo.EntityFrameworkCore.MySql;

namespace Backend.DAL_EF;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    {
        
    }
//    public DbSet<Client> Clients { get; set; }
//    public DbSet<Delivery> Deliveries { get; set; }
//    public DbSet<ServicePoint> ServicePoints { get; set; }
//    public DbSet<Employee> Employees { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<OrderPosition> OrderPositions { get; set; }
    public virtual DbSet<OrderType> OrderTypes { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
 
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
}