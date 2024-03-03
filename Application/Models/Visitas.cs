namespace Application.Models;

public class VisitasModel
{
    public required string id { get; set; }
    public required string id_aula { get; set; }
    public required string sala { get; set; }
    public required string codigo { get; set; }
    public required bool visita { get; set; }
    public required string dataEHorario { get; set; }
    public required string universidade { get; set; }
    public required string campi { get; set; }
}