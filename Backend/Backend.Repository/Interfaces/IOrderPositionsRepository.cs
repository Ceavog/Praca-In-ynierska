using Backend.DataAccessLibrary;
using Backend.Repository.GenericRepositories;

namespace Backend.Repository.Interfaces;

public interface IOrderPositionsRepository : IGenericRepository<OrderPosition>
{
    public void AddOrderPositions(IEnumerable<OrderPosition> orderPositions);
    public IEnumerable<OrderPosition> GetOrderPositionByOrders(int ordeId);
}