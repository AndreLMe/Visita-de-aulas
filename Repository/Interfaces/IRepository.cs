namespace Repository.Interfaces;
public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAll();
    Task<T?> Get(long id);
}