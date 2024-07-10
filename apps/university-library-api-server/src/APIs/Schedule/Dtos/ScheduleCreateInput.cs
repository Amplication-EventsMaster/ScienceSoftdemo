namespace UniversityLibraryApi.APIs.Dtos;

public class ScheduleCreateInput
{
    public string? Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? Date { get; set; }

    public DateTime? Time { get; set; }

    public Room? Room { get; set; }

    public Teacher? Teacher { get; set; }

    public Student? Student { get; set; }
}
