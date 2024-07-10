using UniversityLibraryApi.APIs.Dtos;
using UniversityLibraryApi.Infrastructure.Models;

namespace UniversityLibraryApi.APIs.Extensions;

public static class SchedulesExtensions
{
    public static Schedule ToDto(this ScheduleDbModel model)
    {
        return new Schedule
        {
            Id = model.Id,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            Date = model.Date,
            Time = model.Time,
            Room = model.RoomId,
            Teacher = model.TeacherId,
            Student = model.StudentId,
        };
    }

    public static ScheduleDbModel ToModel(
        this ScheduleUpdateInput updateDto,
        ScheduleWhereUniqueInput uniqueId
    )
    {
        var schedule = new ScheduleDbModel
        {
            Id = uniqueId.Id,
            Date = updateDto.Date,
            Time = updateDto.Time
        };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            schedule.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            schedule.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return schedule;
    }
}
