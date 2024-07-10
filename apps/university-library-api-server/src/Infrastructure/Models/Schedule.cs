using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityLibraryApi.Infrastructure.Models;

[Table("Schedules")]
public class ScheduleDbModel
{
    [Key()]
    [Required()]
    public string Id { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }

    public DateTime? Date { get; set; }

    public DateTime? Time { get; set; }

    public string? RoomId { get; set; }

    [ForeignKey(nameof(RoomId))]
    public RoomDbModel? Room { get; set; } = null;

    public string? TeacherId { get; set; }

    [ForeignKey(nameof(TeacherId))]
    public TeacherDbModel? Teacher { get; set; } = null;

    public string? StudentId { get; set; }

    [ForeignKey(nameof(StudentId))]
    public StudentDbModel? Student { get; set; } = null;
}
