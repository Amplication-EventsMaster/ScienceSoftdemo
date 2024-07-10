using UniversityLibraryApi.APIs.Dtos;
using UniversityLibraryApi.Infrastructure.Models;

namespace UniversityLibraryApi.APIs.Extensions;

public static class TeachersExtensions
{
    public static Teacher ToDto(this TeacherDbModel model)
    {
        return new Teacher
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

    public static TeacherDbModel ToModel(
        this TeacherUpdateInput updateDto,
        TeacherWhereUniqueInput uniqueId
    )
    {
        var teacher = new TeacherDbModel
        {
            Id = uniqueId.Id,
            Name = updateDto.Name,
            Email = updateDto.Email,
            Department = updateDto.Department
        };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            teacher.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            teacher.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return teacher;
    }
}
