using UniversityLibraryApi.APIs.Common;
using UniversityLibraryApi.APIs.Dtos;

namespace UniversityLibraryApi.APIs;

public interface IStudentsService
{
    /// <summary>
    /// Create one Student
    /// </summary>
    public Task<Student> CreateStudent(StudentCreateInput student);

    /// <summary>
    /// Delete one Student
    /// </summary>
    public Task DeleteStudent(StudentWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Students
    /// </summary>
    public Task<List<Student>> Students(StudentFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Student
    /// </summary>
    public Task<Student> Student(StudentWhereUniqueInput uniqueId);

    /// <summary>
    /// Connect multiple Schedules records to Student
    /// </summary>
    public Task ConnectSchedules(
        StudentWhereUniqueInput uniqueId,
        ScheduleWhereUniqueInput[] schedulesId
    );

    /// <summary>
    /// Disconnect multiple Schedules records from Student
    /// </summary>
    public Task DisconnectSchedules(
        StudentWhereUniqueInput uniqueId,
        ScheduleWhereUniqueInput[] schedulesId
    );

    /// <summary>
    /// Find multiple Schedules records for Student
    /// </summary>
    public Task<List<Schedule>> FindSchedules(
        StudentWhereUniqueInput uniqueId,
        ScheduleFindManyArgs ScheduleFindManyArgs
    );

    /// <summary>
    /// Meta data about Student records
    /// </summary>
    public Task<MetadataDto> StudentsMeta(StudentFindManyArgs findManyArgs);

    /// <summary>
    /// Update multiple Schedules records for Student
    /// </summary>
    public Task UpdateSchedules(
        StudentWhereUniqueInput uniqueId,
        ScheduleWhereUniqueInput[] schedulesId
    );

    /// <summary>
    /// Update one Student
    /// </summary>
    public Task UpdateStudent(StudentWhereUniqueInput uniqueId, StudentUpdateInput updateDto);
}
