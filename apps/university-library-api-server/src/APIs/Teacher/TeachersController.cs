using Microsoft.AspNetCore.Mvc;

namespace UniversityLibraryApi.APIs;

[ApiController()]
public class TeachersController : TeachersControllerBase
{
    public TeachersController(ITeachersService service)
        : base(service) { }
}
