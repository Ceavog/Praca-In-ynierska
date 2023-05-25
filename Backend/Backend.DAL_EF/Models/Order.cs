namespace Backend.DataAccessLibrary;

public class Order
{
    public int Id { get; set; }
    // public int EmployeeId { get; set; }
    // public Employee Employee { get; set; }
    
    public DateTime OrderTime { get; set; }
    
    public ICollection<OrderPosition> OrderPositions { get; set; }

    
    // public int ServicePointId { get; set; }
    // public ServicePoint ServicePoint { get; set; }
    
    public int OrderTypeId { get; set; }
    public OrderType OrderType { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}