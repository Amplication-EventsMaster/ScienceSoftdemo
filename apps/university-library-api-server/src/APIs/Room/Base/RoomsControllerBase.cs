using Microsoft.AspNetCore.Mvc;
using UniversityLibraryApi.APIs;
using UniversityLibraryApi.APIs.Common;
using UniversityLibraryApi.APIs.Dtos;
using UniversityLibraryApi.APIs.Errors;

namespace UniversityLibraryApi.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class RoomsControllerBase : ControllerBase
{
    protected readonly IRoomsService _service;

    public RoomsControllerBase(IRoomsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Room
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Room>> CreateRoom(RoomCreateInput input)
    {
        var room = await _service.CreateRoom(input);

        return CreatedAtAction(nameof(Room), new { id = room.Id }, room);
    }

    /// <summary>
    /// Delete one Room
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteRoom([FromRoute()] RoomWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteRoom(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Rooms
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Room>>> Rooms([FromQuery()] RoomFindManyArgs filter)
    {
        return Ok(await _service.Rooms(filter));
    }

    /// <summary>
    /// Get one Room
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Room>> Room([FromRoute()] RoomWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Room(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Connect multiple Schedules records to Room
    /// </summary>
    [HttpPost("{Id}/schedules")]
    public async Task<ActionResult> ConnectSchedules(
        [FromRoute()] RoomWhereUniqueInput uniqueId,
        [FromQuery()] ScheduleWhereUniqueInput[] schedulesId
    )
    {
        try
        {
            await _service.ConnectSchedules(uniqueId, schedulesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Schedules records from Room
    /// </summary>
    [HttpDelete("{Id}/schedules")]
    public async Task<ActionResult> DisconnectSchedules(
        [FromRoute()] RoomWhereUniqueInput uniqueId,
        [FromBody()] ScheduleWhereUniqueInput[] schedulesId
    )
    {
        try
        {
            await _service.DisconnectSchedules(uniqueId, schedulesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Schedules records for Room
    /// </summary>
    [HttpGet("{Id}/schedules")]
    public async Task<ActionResult<List<Schedule>>> FindSchedules(
        [FromRoute()] RoomWhereUniqueInput uniqueId,
        [FromQuery()] ScheduleFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindSchedules(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Meta data about Room records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> RoomsMeta([FromQuery()] RoomFindManyArgs filter)
    {
        return Ok(await _service.RoomsMeta(filter));
    }

    /// <summary>
    /// Update multiple Schedules records for Room
    /// </summary>
    [HttpPatch("{Id}/schedules")]
    public async Task<ActionResult> UpdateSchedules(
        [FromRoute()] RoomWhereUniqueInput uniqueId,
        [FromBody()] ScheduleWhereUniqueInput[] schedulesId
    )
    {
        try
        {
            await _service.UpdateSchedules(uniqueId, schedulesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Update one Room
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateRoom(
        [FromRoute()] RoomWhereUniqueInput uniqueId,
        [FromQuery()] RoomUpdateInput roomUpdateDto
    )
    {
        try
        {
            await _service.UpdateRoom(uniqueId, roomUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
