using UniversityLibraryApi.Infrastructure;

namespace UniversityLibraryApi.APIs;

public class StudentsService : StudentsServiceBase
{
    public StudentsService(UniversityLibraryApiDbContext context)
        : base(context) { }
}
