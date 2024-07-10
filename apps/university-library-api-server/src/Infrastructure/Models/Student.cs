using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityLibraryApi.Infrastructure.Models;

[Table("Students")]
public class StudentDbModel
{
    [Key()]
    [Required()]
    public string Id { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }

    [StringLength(1000)]
    public string? Name { get; set; }

    public string? Email { get; set; }

    [StringLength(1000)]
    public string? Department { get; set; }

    public List<ScheduleDbModel>? Schedules { get; set; } = new List<ScheduleDbModel>();
}
