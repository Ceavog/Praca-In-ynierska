using Backend.DataAccessLibrary;
using Backend.Repository.GenericRepositories;

namespace Backend.Repository.Interfaces;

public interface IOrderRepository : IGenericRepository<Order>
{
    int AddOrder(Order order);
    IEnumerable<Order> GetOrdersByUserId(int UserId);
}