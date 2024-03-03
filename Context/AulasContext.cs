using Application.Models;
using Microsoft.EntityFrameworkCore;
namespace Context;
public class AulasContext : DbContext
{
    public DbSet<AulasModel> Aulas { get; set; }
    private readonly string dbConnection;
    public AulasContext()
    {
        dbConnection = @$"./{Environment.GetEnvironmentVariable("AULAS_DB")}";
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={dbConnection}");
}