using Application.Models;

namespace Repository.Interfaces;
public interface IVisitas:IRepository<VisitasModel>
{
    Task Add(VisitasModel item);

    Task Edit(VisitasModel item);

    Task Delete(string id);
}