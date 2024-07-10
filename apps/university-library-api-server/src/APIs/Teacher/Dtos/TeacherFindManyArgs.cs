using Microsoft.AspNetCore.Mvc;
using UniversityLibraryApi.APIs.Common;
using UniversityLibraryApi.Infrastructure.Models;

namespace UniversityLibraryApi.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class TeacherFindManyArgs : FindManyInput<Teacher, TeacherWhereInput> { }
