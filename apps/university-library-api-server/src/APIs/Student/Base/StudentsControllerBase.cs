using Microsoft.AspNetCore.Mvc;
using UniversityLibraryApi.APIs;
using UniversityLibraryApi.APIs.Common;
using UniversityLibraryApi.APIs.Dtos;
using UniversityLibraryApi.APIs.Errors;

namespace UniversityLibraryApi.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class StudentsControllerBase : ControllerBase
{
    protected readonly IStudentsService _service;

    public StudentsControllerBase(IStudentsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Student
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Student>> CreateStudent(StudentCreateInput input)
    {
        var student = await _service.CreateStudent(input);

        return CreatedAtAction(nameof(Student), new { id = student.Id }, student);
    }

    /// <summary>
    /// Delete one Student
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteStudent([FromRoute()] StudentWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteStudent(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Students
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Student>>> Students(
        [FromQuery()] StudentFindManyArgs filter
    )
    {
        return Ok(await _service.Students(filter));
    }

    /// <summary>
    /// Get one Student
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Student>> Student([FromRoute()] StudentWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Student(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Connect multiple Schedules records to Student
    /// </summary>
    [HttpPost("{Id}/schedules")]
    public async Task<ActionResult> ConnectSchedules(
        [FromRoute()] StudentWhereUniqueInput uniqueId,
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
    /// Disconnect multiple Schedules records from Student
    /// </summary>
    [HttpDelete("{Id}/schedules")]
    public async Task<ActionResult> DisconnectSchedules(
        [FromRoute()] StudentWhereUniqueInput uniqueId,
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
    /// Find multiple Schedules records for Student
    /// </summary>
    [HttpGet("{Id}/schedules")]
    public async Task<ActionResult<List<Schedule>>> FindSchedules(
        [FromRoute()] StudentWhereUniqueInput uniqueId,
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
    /// Meta data about Student records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> StudentsMeta(
        [FromQuery()] StudentFindManyArgs filter
    )
    {
        return Ok(await _service.StudentsMeta(filter));
    }

    /// <summary>
    /// Update multiple Schedules records for Student
    /// </summary>
    [HttpPatch("{Id}/schedules")]
    public async Task<ActionResult> UpdateSchedules(
        [FromRoute()] StudentWhereUniqueInput uniqueId,
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
    /// Update one Student
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateStudent(
        [FromRoute()] StudentWhereUniqueInput uniqueId,
        [FromQuery()] StudentUpdateInput studentUpdateDto
    )
    {
        try
        {
            await _service.UpdateStudent(uniqueId, studentUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
