namespace Backend.DTOs;
public class ProblemListDto
{
    public List<ProblemDto> Problems { get; set; } = new();
    public int ProblemCount { get; set; }

}