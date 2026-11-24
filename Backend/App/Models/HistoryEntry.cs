namespace Backend.Models;
using Microsoft.EntityFrameworkCore;

[PrimaryKey(nameof(UserId), nameof(ProblemId))]
public class HistoryEntry
{
    public bool IsCorrect { get; set; }
    
    public DateTime FirstAttemptAt { get; set; } = DateTime.UtcNow; //pierwsze podejście do zadania
    public DateTime LastAttemptAt { get; set; } = DateTime.UtcNow; //ostatnie podejście do zadania
    
    public int AttemptsToSolve {get; set;} = 1; //zlicza próby aż do pierwszej udanej
    public int TotalAttempts {get; set;} = 1; //zlicza wszystkie podejścia (udane i nieudane)
    public DateTime? FirstSolvedAt { get; set; }

    public Guid UserId { get; set; }
    public AppUser User { get; set; } = null!; //klucz nawigacyjny

    public Guid ProblemId { get; set; }
    public Problem Problem { get; set; } = null!; //klucz nawigacyjny
}
