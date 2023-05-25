using Backend.DAL_EF;
using Backend.DataAccessLibrary;
using Backend.Repository.GenericRepositories;
using Backend.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository.Repositories;

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    private readonly ApplicationDbContext _applicationDbContext;
    public OrderRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public int AddOrder(Order order)
    {
        _applicationDbContext.Orders.Add(order);
        _applicationDbContext.SaveChanges();
        return order.Id;
    }
    public IEnumerable<Order> GetOrdersByUserId(int UserId)
    {
        var x = _applicationDbContext.Orders.Include(x=>x.OrderPositions).Where(x=>x.UserId == UserId && x.OrderTime.Date == DateTime.Today).ToList();
        return x;
    }
} 