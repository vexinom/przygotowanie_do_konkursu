namespace Backend.DTOs;

public class HistoryFilterDto
{   
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public bool? IsCorrect { get; set; }
}