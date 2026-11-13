namespace Backend.Models;

public class Category
{
    public Guid  Id { get; set; } = Guid.NewGuid();
    public bool IsDeleted { get; set; } = false;
    public string Name {get; set;} = string.Empty;
    public ICollection<Problem> Problems { get; set; } = new List<Problem>();

}