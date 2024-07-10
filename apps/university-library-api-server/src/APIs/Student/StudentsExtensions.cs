using UniversityLibraryApi.APIs.Dtos;
using UniversityLibraryApi.Infrastructure.Models;

namespace UniversityLibraryApi.APIs.Extensions;

public static class StudentsExtensions
{
    public static Student ToDto(this StudentDbModel model)
    {
        return new Student
        {
            Id = model.Id,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            Name = model.Name,
            Email = model.Email,
            Department = model.Department,
            Schedules = model.Schedules?.Select(x => x.Id).ToList(),
        };
    }

    public static StudentDbModel ToModel(
        this StudentUpdateInput updateDto,
        StudentWhereUniqueInput uniqueId
    )
    {
        var student = new StudentDbModel
        {
            Id = uniqueId.Id,
            Name = updateDto.Name,
            Email = updateDto.Email,
            Department = updateDto.Department
        };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            student.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            student.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return student;
    }
}
