using UniversityLibraryApi.Infrastructure;

namespace UniversityLibraryApi.APIs;

public class TeachersService : TeachersServiceBase
{
    public TeachersService(UniversityLibraryApiDbContext context)
        : base(context) { }
}
