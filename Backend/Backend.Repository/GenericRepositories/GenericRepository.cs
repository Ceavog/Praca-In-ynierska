using Backend.DAL_EF;

namespace Backend.Repository.GenericRepositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly ApplicationDbContext _applicationDbContext;
    public GenericRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
    public IEnumerable<T> GetAll()
    {
        return _applicationDbContext.Set<T>().ToList();
    }

    public T? Get(int id)
    {
        var item =_applicationDbContext.Set<T>().Find(id);
        if (item == null)
        {
            throw new Exception("Cant find entity with that id");
        }

        return item;
    }

    public T Add(T entity)
    {
        var addedEntity = _applicationDbContext.Set<T>().Add(entity);
        _applicationDbContext.SaveChanges();
        return addedEntity.Entity;
    }

    public T Update(T entity)
    {
        var updatedEntity = _applicationDbContext.Set<T>().Update(entity);
        _applicationDbContext.SaveChanges();
        return updatedEntity.Entity;
    }

    public T Delete(int id)
    {
        var entityToDelete = _applicationDbContext.Set<T>().Find(id);
        
        if (entityToDelete == null) return null;
        
        var deletedEntity = _applicationDbContext.Set<T>().Remove(entityToDelete);
        _applicationDbContext.SaveChanges();
        return deletedEntity.Entity;

    }
}