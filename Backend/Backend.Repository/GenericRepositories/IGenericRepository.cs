namespace Backend.Repository.GenericRepositories;

public interface IGenericRepository<T> where T : class
{
    IEnumerable<T> GetAll();
    T? Get(int id);
    T Add(T entity);
    T Update(T entity);
    T Delete(int id);
    
}