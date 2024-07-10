using UniversityLibraryApi.APIs.Common;
using UniversityLibraryApi.APIs.Dtos;

namespace UniversityLibraryApi.APIs;

public interface ISchedulesService
{
    /// <summary>
    /// Create one Schedule
    /// </summary>
    public Task<Schedule> CreateSchedule(ScheduleCreateInput schedule);

    /// <summary>
    /// Delete one Schedule
    /// </summary>
    public Task DeleteSchedule(ScheduleWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Schedules
    /// </summary>
    public Task<List<Schedule>> Schedules(ScheduleFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Schedule
    /// </summary>
    public Task<Schedule> Schedule(ScheduleWhereUniqueInput uniqueId);

    /// <summary>
    /// Get a Room record for Schedule
    /// </summary>
    public Task<Room> GetRoom(ScheduleWhereUniqueInput uniqueId);

    /// <summary>
    /// Get a Student record for Schedule
    /// </summary>
    public Task<Student> GetStudent(ScheduleWhereUniqueInput uniqueId);

    /// <summary>
    /// Get a Teacher record for Schedule
    /// </summary>
    public Task<Teacher> GetTeacher(ScheduleWhereUniqueInput uniqueId);

    /// <summary>
    /// Meta data about Schedule records
    /// </summary>
    public Task<MetadataDto> SchedulesMeta(ScheduleFindManyArgs findManyArgs);

    /// <summary>
    /// Update one Schedule
    /// </summary>
    public Task UpdateSchedule(ScheduleWhereUniqueInput uniqueId, ScheduleUpdateInput updateDto);
}
