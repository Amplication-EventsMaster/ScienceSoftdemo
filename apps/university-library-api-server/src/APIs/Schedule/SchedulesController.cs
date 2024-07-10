using Microsoft.AspNetCore.Mvc;

namespace UniversityLibraryApi.APIs;

[ApiController()]
public class SchedulesController : SchedulesControllerBase
{
    public SchedulesController(ISchedulesService service)
        : base(service) { }
}
