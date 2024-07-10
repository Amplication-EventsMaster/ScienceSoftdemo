using Microsoft.AspNetCore.Mvc;
using UniversityLibraryApi.APIs.Common;
using UniversityLibraryApi.Infrastructure.Models;

namespace UniversityLibraryApi.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class ScheduleFindManyArgs : FindManyInput<Schedule, ScheduleWhereInput> { }
