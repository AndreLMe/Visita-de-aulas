using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Interfaces;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    public IAulas Aulas {get;}
    private readonly DbContext _context;
    public IVisitas Visitas {get;}
    public IRequest Requests { get; }

    public UnitOfWork(DbContext dbContext)
    {
        _context = dbContext;

        Aulas = new AulasRepository(dbContext);
        Visitas = new VisitasRepository(dbContext);
        Requests = new RequestRepository();
    }
    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose() => _context.Dispose();
    
}