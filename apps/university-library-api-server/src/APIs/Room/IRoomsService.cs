using UniversityLibraryApi.APIs.Common;
using UniversityLibraryApi.APIs.Dtos;

namespace UniversityLibraryApi.APIs;

public interface IRoomsService
{
    /// <summary>
    /// Create one Room
    /// </summary>
    public Task<Room> CreateRoom(RoomCreateInput room);

    /// <summary>
    /// Delete one Room
    /// </summary>
    public Task DeleteRoom(RoomWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Rooms
    /// </summary>
    public Task<List<Room>> Rooms(RoomFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Room
    /// </summary>
    public Task<Room> Room(RoomWhereUniqueInput uniqueId);

    /// <summary>
    /// Connect multiple Schedules records to Room
    /// </summary>
    public Task ConnectSchedules(
        RoomWhereUniqueInput uniqueId,
        ScheduleWhereUniqueInput[] schedulesId
    );

    /// <summary>
    /// Disconnect multiple Schedules records from Room
    /// </summary>
    public Task DisconnectSchedules(
        RoomWhereUniqueInput uniqueId,
        ScheduleWhereUniqueInput[] schedulesId
    );

    /// <summary>
    /// Find multiple Schedules records for Room
    /// </summary>
    public Task<List<Schedule>> FindSchedules(
        RoomWhereUniqueInput uniqueId,
        ScheduleFindManyArgs ScheduleFindManyArgs
    );

    /// <summary>
    /// Meta data about Room records
    /// </summary>
    public Task<MetadataDto> RoomsMeta(RoomFindManyArgs findManyArgs);

    /// <summary>
    /// Update multiple Schedules records for Room
    /// </summary>
    public Task UpdateSchedules(
        RoomWhereUniqueInput uniqueId,
        ScheduleWhereUniqueInput[] schedulesId
    );

    /// <summary>
    /// Update one Room
    /// </summary>
    public Task UpdateRoom(RoomWhereUniqueInput uniqueId, RoomUpdateInput updateDto);
}
