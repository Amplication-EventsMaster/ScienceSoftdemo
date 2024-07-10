using Microsoft.AspNetCore.Mvc;
using UniversityLibraryApi.APIs;
using UniversityLibraryApi.APIs.Common;
using UniversityLibraryApi.APIs.Dtos;
using UniversityLibraryApi.APIs.Errors;

namespace UniversityLibraryApi.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class SchedulesControllerBase : ControllerBase
{
    protected readonly ISchedulesService _service;

    public SchedulesControllerBase(ISchedulesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Schedule
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Schedule>> CreateSchedule(ScheduleCreateInput input)
    {
        var schedule = await _service.CreateSchedule(input);

        return CreatedAtAction(nameof(Schedule), new { id = schedule.Id }, schedule);
    }

    /// <summary>
    /// Delete one Schedule
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteSchedule([FromRoute()] ScheduleWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteSchedule(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Schedules
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Schedule>>> Schedules(
        [FromQuery()] ScheduleFindManyArgs filter
    )
    {
        return Ok(await _service.Schedules(filter));
    }

    /// <summary>
    /// Get one Schedule
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Schedule>> Schedule(
        [FromRoute()] ScheduleWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Schedule(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Get a Room record for Schedule
    /// </summary>
    [HttpGet("{Id}/rooms")]
    public async Task<ActionResult<List<Room>>> GetRoom(
        [FromRoute()] ScheduleWhereUniqueInput uniqueId
    )
    {
        var room = await _service.GetRoom(uniqueId);
        return Ok(room);
    }

    /// <summary>
    /// Get a Student record for Schedule
    /// </summary>
    [HttpGet("{Id}/students")]
    public async Task<ActionResult<List<Student>>> GetStudent(
        [FromRoute()] ScheduleWhereUniqueInput uniqueId
    )
    {
        var student = await _service.GetStudent(uniqueId);
        return Ok(student);
    }

    /// <summary>
    /// Get a Teacher record for Schedule
    /// </summary>
    [HttpGet("{Id}/teachers")]
    public async Task<ActionResult<List<Teacher>>> GetTeacher(
        [FromRoute()] ScheduleWhereUniqueInput uniqueId
    )
    {
        var teacher = await _service.GetTeacher(uniqueId);
        return Ok(teacher);
    }

    /// <summary>
    /// Meta data about Schedule records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> SchedulesMeta(
        [FromQuery()] ScheduleFindManyArgs filter
    )
    {
        return Ok(await _service.SchedulesMeta(filter));
    }

    /// <summary>
    /// Update one Schedule
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateSchedule(
        [FromRoute()] ScheduleWhereUniqueInput uniqueId,
        [FromQuery()] ScheduleUpdateInput scheduleUpdateDto
    )
    {
        try
        {
            await _service.UpdateSchedule(uniqueId, scheduleUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
