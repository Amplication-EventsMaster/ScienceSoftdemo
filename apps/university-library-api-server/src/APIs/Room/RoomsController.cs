using Microsoft.AspNetCore.Mvc;

namespace UniversityLibraryApi.APIs;

[ApiController()]
public class RoomsController : RoomsControllerBase
{
    public RoomsController(IRoomsService service)
        : base(service) { }
}
