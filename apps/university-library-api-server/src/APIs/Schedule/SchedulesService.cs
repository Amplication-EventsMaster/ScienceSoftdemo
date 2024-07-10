using UniversityLibraryApi.Infrastructure;

namespace UniversityLibraryApi.APIs;

public class SchedulesService : SchedulesServiceBase
{
    public SchedulesService(UniversityLibraryApiDbContext context)
        : base(context) { }
}
