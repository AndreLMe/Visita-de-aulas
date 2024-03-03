using Application.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

public class VisitasRepository : GenericRepository<VisitasModel>, IVisitas
{
    public VisitasRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public Task Add(VisitasModel item)
    {
        throw new NotImplementedException();
    }

    public Task Delete(string id)
    {
        throw new NotImplementedException();
    }

    public Task Edit(VisitasModel item)
    {
        throw new NotImplementedException();
    }

    public Task<VisitasModel> Get(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<VisitasModel>> GetAll()
    {
        throw new NotImplementedException();
    }
}