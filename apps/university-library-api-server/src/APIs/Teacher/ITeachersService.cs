using UniversityLibraryApi.APIs.Common;
using UniversityLibraryApi.APIs.Dtos;

namespace UniversityLibraryApi.APIs;

public interface ITeachersService
{
    /// <summary>
    /// Create one Teacher
    /// </summary>
    public Task<Teacher> CreateTeacher(TeacherCreateInput teacher);

    /// <summary>
    /// Delete one Teacher
    /// </summary>
    public Task DeleteTeacher(TeacherWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Teachers
    /// </summary>
    public Task<List<Teacher>> Teachers(TeacherFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Teacher
    /// </summary>
    public Task<Teacher> Teacher(TeacherWhereUniqueInput uniqueId);

    /// <summary>
    /// Connect multiple Schedules records to Teacher
    /// </summary>
    public Task ConnectSchedules(
        TeacherWhereUniqueInput uniqueId,
        ScheduleWhereUniqueInput[] schedulesId
    );

    /// <summary>
    /// Disconnect multiple Schedules records from Teacher
    /// </summary>
    public Task DisconnectSchedules(
        TeacherWhereUniqueInput uniqueId,
        ScheduleWhereUniqueInput[] schedulesId
    );

    /// <summary>
    /// Find multiple Schedules records for Teacher
    /// </summary>
    public Task<List<Schedule>> FindSchedules(
        TeacherWhereUniqueInput uniqueId,
        ScheduleFindManyArgs ScheduleFindManyArgs
    );

    /// <summary>
    /// Meta data about Teacher records
    /// </summary>
    public Task<MetadataDto> TeachersMeta(TeacherFindManyArgs findManyArgs);

    /// <summary>
    /// Update multiple Schedules records for Teacher
    /// </summary>
    public Task UpdateSchedules(
        TeacherWhereUniqueInput uniqueId,
        ScheduleWhereUniqueInput[] schedulesId
    );

    /// <summary>
    /// Update one Teacher
    /// </summary>
    public Task UpdateTeacher(TeacherWhereUniqueInput uniqueId, TeacherUpdateInput updateDto);
}
