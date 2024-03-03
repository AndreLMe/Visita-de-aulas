using Application.Models;
using Repository.Interfaces;

namespace Repository;
public class RequestRepository : IRequest
{
    private static Dictionary<long, Request> requests = new Dictionary<long, Request>();

    public async Task Add(long id, Request request)
    {
        await Task.Run(() => requests.TryAdd(id, request));
    }

    public bool Contains(long id)
    {
        return requests.ContainsKey(id);
    }

    public async Task Edit(long id, Request request)
    {
        await Task.Run(() => {
            Request req;
            if(requests.TryGetValue(id, out req))
            {
                if(!String.IsNullOrEmpty(request.ME))
                    req.ME = request.ME;
                else if(!String.IsNullOrEmpty(request.Campi))
                    req.Campi = request.Campi;
                else if(!String.IsNullOrEmpty(request.Universidade))
                    req.Universidade = request.Universidade;
                else if(!String.IsNullOrEmpty(request.Horario))
                    req.Horario = request.Horario;
            }
        });
    }

    public async Task Edit(long id, Request request, string nameof)
    {
        Request req;
        if(requests.TryGetValue(id, out req))
            req.EstadoAnterior = nameof;
        await this.Edit(id, request);
    }

    public async Task<Request?> Get(string id)
    {
        return await Task.Run(() => requests.GetValueOrDefault(long.Parse(id)));
    }

    public async Task<Request?> Get(long id)
    {
        return await Task.Run(() => requests.GetValueOrDefault(id));
    }

    public async Task<IEnumerable<Request>> GetAll()
    {
        return await Task.Run(() => requests.Values.ToList());
    }

    public void Remove(long id)
    {
        requests.Remove(id);
    }
}