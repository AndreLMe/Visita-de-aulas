using Application.Models;

namespace Repository.Interfaces;

public interface IRequest : IRepository<Request>
{
    Task Add(long id, Request request);
    Task Edit(long id, Request request);
    Task Edit(long id, Request request, string nameof);
    void Remove(long id);
    bool Contains(long id);
}