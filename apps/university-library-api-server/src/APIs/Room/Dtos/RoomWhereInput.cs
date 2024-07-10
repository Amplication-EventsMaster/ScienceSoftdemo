namespace UniversityLibraryApi.APIs.Dtos;

public class RoomWhereInput
{
    public string? Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? RoomNumber { get; set; }

    public int? Capacity { get; set; }

    public List<string>? Schedules { get; set; }
}
