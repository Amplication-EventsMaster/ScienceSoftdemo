using Microsoft.AspNetCore.Mvc;

namespace UniversityLibraryApi.APIs;

[ApiController()]
public class StudentsController : StudentsControllerBase
{
    public StudentsController(IStudentsService service)
        : base(service) { }
}
