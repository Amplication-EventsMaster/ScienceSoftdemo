using Microsoft.AspNetCore.Mvc;
using UniversityLibraryApi.APIs;
using UniversityLibraryApi.APIs.Common;
using UniversityLibraryApi.APIs.Dtos;
using UniversityLibraryApi.APIs.Errors;

namespace UniversityLibraryApi.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class TeachersControllerBase : ControllerBase
{
    protected readonly ITeachersService _service;

    public TeachersControllerBase(ITeachersService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Teacher
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Teacher>> CreateTeacher(TeacherCreateInput input)
    {
        var teacher = await _service.CreateTeacher(input);

        return CreatedAtAction(nameof(Teacher), new { id = teacher.Id }, teacher);
    }

    /// <summary>
    /// Delete one Teacher
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteTeacher([FromRoute()] TeacherWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteTeacher(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Teachers
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Teacher>>> Teachers(
        [FromQuery()] TeacherFindManyArgs filter
    )
    {
        return Ok(await _service.Teachers(filter));
    }

    /// <summary>
    /// Get one Teacher
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Teacher>> Teacher([FromRoute()] TeacherWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Teacher(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Connect multiple Schedules records to Teacher
    /// </summary>
    [HttpPost("{Id}/schedules")]
    public async Task<ActionResult> ConnectSchedules(
        [FromRoute()] TeacherWhereUniqueInput uniqueId,
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
    /// Disconnect multiple Schedules records from Teacher
    /// </summary>
    [HttpDelete("{Id}/schedules")]
    public async Task<ActionResult> DisconnectSchedules(
        [FromRoute()] TeacherWhereUniqueInput uniqueId,
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
    /// Find multiple Schedules records for Teacher
    /// </summary>
    [HttpGet("{Id}/schedules")]
    public async Task<ActionResult<List<Schedule>>> FindSchedules(
        [FromRoute()] TeacherWhereUniqueInput uniqueId,
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
    /// Meta data about Teacher records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> TeachersMeta(
        [FromQuery()] TeacherFindManyArgs filter
    )
    {
        return Ok(await _service.TeachersMeta(filter));
    }

    /// <summary>
    /// Update multiple Schedules records for Teacher
    /// </summary>
    [HttpPatch("{Id}/schedules")]
    public async Task<ActionResult> UpdateSchedules(
        [FromRoute()] TeacherWhereUniqueInput uniqueId,
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
    /// Update one Teacher
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateTeacher(
        [FromRoute()] TeacherWhereUniqueInput uniqueId,
        [FromQuery()] TeacherUpdateInput teacherUpdateDto
    )
    {
        try
        {
            await _service.UpdateTeacher(uniqueId, teacherUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
