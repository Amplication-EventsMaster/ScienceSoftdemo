using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityLibraryApi.Infrastructure.Models;

[Table("Rooms")]
public class RoomDbModel
{
    [Key()]
    [Required()]
    public string Id { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }

    [StringLength(1000)]
    public string? RoomNumber { get; set; }

    [Range(-999999999, 999999999)]
    public int? Capacity { get; set; }

    public List<ScheduleDbModel>? Schedules { get; set; } = new List<ScheduleDbModel>();
}
