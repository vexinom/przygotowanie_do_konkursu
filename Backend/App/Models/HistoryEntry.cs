namespace Backend.Models;

public enum SolveState
{
    Correct,
    Wrong,
    InProgress
}

public class HistoryEntry
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsDeleted { get; set; } = false;
    public SolveState State { get; set; } = SolveState.InProgress;
    public DateTime LastInteractionTime { get; set; } = DateTime.Now;

    // public Guid UserId { get; set; }
    // public User User { get; set; } = null!; //klucz nawigacyjny

    public Guid ProblemId { get; set; }
    public Problem Problem { get; set; } = null!; //klucz nawigacyjny
}
