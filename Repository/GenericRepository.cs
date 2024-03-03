using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.Interfaces;

public class GenericRepository<T> : IRepository<T> where T : class
{
    //private readonly ILogger _logger;
    protected DbContext _dbContext;
    internal DbSet<T> _dbSet;
    public GenericRepository(
        DbContext dbContext)
    {
        _dbContext = dbContext;
        //_logger = logger;
        _dbSet = _dbContext.Set<T>();
    }
    public async Task<T?> Get(string id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<T?> Get(long id)
    {
        return await _dbSet.FindAsync(id);
    }

    public Task<IEnumerable<T>> GetAll()
    {
        throw new NotImplementedException();
    }
}