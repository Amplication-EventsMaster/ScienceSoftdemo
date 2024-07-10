namespace UniversityLibraryApi.APIs.Dtos;

public class ScheduleWhereInput
{
    public string? Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? Date { get; set; }

    public DateTime? Time { get; set; }

    public string? Room { get; set; }

    public string? Teacher { get; set; }

    public string? Student { get; set; }
}
