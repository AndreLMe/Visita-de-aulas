namespace Repository.Interfaces;
public interface IUnitOfWork
{
    IAulas Aulas {get;}
    IVisitas Visitas {get;}
    IRequest Requests {get;}
    Task CompleteAsync();
}