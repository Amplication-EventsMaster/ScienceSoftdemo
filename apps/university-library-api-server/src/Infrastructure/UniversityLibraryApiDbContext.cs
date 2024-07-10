using Microsoft.EntityFrameworkCore;
using UniversityLibraryApi.Infrastructure.Models;

namespace UniversityLibraryApi.Infrastructure;

public class UniversityLibraryApiDbContext : DbContext
{
    public UniversityLibraryApiDbContext(DbContextOptions<UniversityLibraryApiDbContext> options)
        : base(options) { }

    public DbSet<TeacherDbModel> Teachers { get; set; }

    public DbSet<ScheduleDbModel> Schedules { get; set; }

    public DbSet<RoomDbModel> Rooms { get; set; }

    public DbSet<StudentDbModel> Students { get; set; }
}
