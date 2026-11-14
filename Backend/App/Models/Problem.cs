namespace Backend.Models;

public class Problem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Content { get; set;} = string.Empty;
    public bool IsDeleted { get; set; } = false;
    
    
    public Guid? ContestId { get; set; }
    public Contest? Contest { get; set; }

    
    public ICollection<Answer> Answers { get; set; } = new List<Answer>();
}