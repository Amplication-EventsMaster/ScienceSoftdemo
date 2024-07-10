namespace UniversityLibraryApi.APIs.Dtos;

public class Student
{
    public string Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Department { get; set; }

    public List<string>? Schedules { get; set; }
}
