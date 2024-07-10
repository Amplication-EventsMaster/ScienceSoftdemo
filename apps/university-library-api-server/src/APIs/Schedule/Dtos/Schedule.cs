namespace UniversityLibraryApi.APIs.Dtos;

public class Schedule
{
    public string Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? Date { get; set; }

    public DateTime? Time { get; set; }

    public string? Room { get; set; }

    public string? Teacher { get; set; }

    public string? Student { get; set; }
}
