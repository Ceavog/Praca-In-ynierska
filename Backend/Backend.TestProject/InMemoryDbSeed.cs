using Backend.DAL_EF;
using Backend.DataAccessLibrary;
using Microsoft.EntityFrameworkCore;

namespace Backend.TestProject;

public static class InMemoryDbSeed
{
    public static ApplicationDbContext SeedDbContext()
    {
        var dbContextOptionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        dbContextOptionsBuilder.UseInMemoryDatabase("Test");
        var dbContext = new ApplicationDbContext(dbContextOptionsBuilder.Options);

        dbContext.Users.Add(new User
        {
            Id = 1,
            Login = "login1",
            Password = BCrypt.Net.BCrypt.HashPassword("password")
        });
    
        dbContext.Users.Add(new User
        {
            Id = 2,
            Login = "1234",
            Password = BCrypt.Net.BCrypt.HashPassword("1234")
        });
        
        dbContext.Categories.Add(new Category()
        {
            Id = 1,
            Name = "category1",
            UserId = 1,
        });

        dbContext.Products.Add(new Products()
        {
            Id = 1,
            CategoryId = 1,
            UserId = 1,
            Name = "product1",
            Price = 100.0,
            Section = "section1",
        });

        dbContext.RefreshTokens.Add(new RefreshToken()
        {
            ExpirationTime = DateTime.Now.AddDays(-10),
            Token = "DxfAmLujQwIj2zARTc5pNNLWFH8IRTyGz0zQAE9UoFM%3D",
            UserId = 2,
        });
        dbContext.SaveChanges();

        return dbContext;
    }
}