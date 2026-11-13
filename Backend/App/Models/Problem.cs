namespace Backend.Models;

public class Problem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Content { get; set;} = string.Empty;
    public bool IsDeleted { get; set; } = false;
    public ICollection<Category> Categories { get; set; } = new List<Category>();
    //nie wiem czy tu ^ nie będzie potrzebny virtual do LazyLoading, ale zobaczymy...
    public ICollection<Answer> Answers { get; set; } = new List<Answer>();
}