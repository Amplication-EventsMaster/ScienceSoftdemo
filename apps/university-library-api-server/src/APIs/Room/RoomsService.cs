using UniversityLibraryApi.Infrastructure;

namespace UniversityLibraryApi.APIs;

public class RoomsService : RoomsServiceBase
{
    public RoomsService(UniversityLibraryApiDbContext context)
        : base(context) { }
}
