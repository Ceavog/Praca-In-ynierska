using Backend.DAL_EF;
using Backend.DataAccessLibrary;
using Backend.Repository.GenericRepositories;
using Backend.Repository.Interfaces;

namespace Backend.Repository.Repositories;

public class OrderPositionsReporistory : GenericRepository<OrderPosition>,  IOrderPositionsRepository
{
    private readonly ApplicationDbContext _applicationDbContext;
    public OrderPositionsReporistory(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public void AddOrderPositions(IEnumerable<OrderPosition> orderPositions)
    {
        _applicationDbContext.OrderPositions.AddRange(orderPositions);
        _applicationDbContext.SaveChanges();
    }

    public IEnumerable<OrderPosition> GetOrderPositionByOrders(int orderId)
    {
        return _applicationDbContext.OrderPositions.Where(x => x.OrderId.Equals(orderId));
    }
}