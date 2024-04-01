using Application.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Repository;
public class AulasRepository : GenericRepository<AulasModel>, IAulas
{
    public AulasRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<AulasModel>> GetPorHorarioECampi(
        string dataEHorario,
        string campi,
        string dia)
        {
            Console.WriteLine($"das {dataEHorario}, {dia}");
            dia = "segunda";
            return await _dbSet.Where(t => 
            t.universidade == "UFABC" &&
            t.campi == campi &&
            t.dataEHorario.Contains($"das {dataEHorario}") &&
            t.dataEHorario.Contains(dia))
            .AsNoTracking()
            .AsSplitQuery()
            .ToListAsync();
        }
}