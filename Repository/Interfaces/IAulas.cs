using Application.Models;
namespace Repository.Interfaces;
public interface IAulas:IRepository<AulasModel>
{
    Task<List<AulasModel>> GetPorHorarioECampi(
        string dataEHorario,
        string campi,
        string dia);
}