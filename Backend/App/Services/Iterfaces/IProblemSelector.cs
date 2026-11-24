namespace Backend.Services;

public interface IProblemSelector
{
    /// Zwraca Id następnego zadania dla albo null, gdy nie ma żadnego.
    Task<Guid?> SelectNextProblemId(Guid userId);
}