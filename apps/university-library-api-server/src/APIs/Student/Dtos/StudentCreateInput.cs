namespace UniversityLibraryApi.APIs.Dtos;

public class StudentCreateInput
{
    public string? Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Department { get; set; }

    public List<Schedule>? Schedules { get; set; }
}
