using Microsoft.EntityFrameworkCore;
using UniversityLibraryApi.APIs;
using UniversityLibraryApi.APIs.Common;
using UniversityLibraryApi.APIs.Dtos;
using UniversityLibraryApi.APIs.Errors;
using UniversityLibraryApi.APIs.Extensions;
using UniversityLibraryApi.Infrastructure;
using UniversityLibraryApi.Infrastructure.Models;

namespace UniversityLibraryApi.APIs;

public abstract class SchedulesServiceBase : ISchedulesService
{
    protected readonly UniversityLibraryApiDbContext _context;

    public SchedulesServiceBase(UniversityLibraryApiDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Schedule
    /// </summary>
    public async Task<Schedule> CreateSchedule(ScheduleCreateInput createDto)
    {
        var schedule = new ScheduleDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt,
            Date = createDto.Date,
            Time = createDto.Time
        };

        if (createDto.Id != null)
        {
            schedule.Id = createDto.Id;
        }
        if (createDto.Teacher != null)
        {
            schedule.Teacher = await _context
                .Teachers.Where(teacher => createDto.Teacher.Id == teacher.Id)
                .FirstOrDefaultAsync();
        }

        if (createDto.Room != null)
        {
            schedule.Room = await _context
                .Rooms.Where(room => createDto.Room.Id == room.Id)
                .FirstOrDefaultAsync();
        }

        if (createDto.Student != null)
        {
            schedule.Student = await _context
                .Students.Where(student => createDto.Student.Id == student.Id)
                .FirstOrDefaultAsync();
        }

        _context.Schedules.Add(schedule);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<ScheduleDbModel>(schedule.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Schedule
    /// </summary>
    public async Task DeleteSchedule(ScheduleWhereUniqueInput uniqueId)
    {
        var schedule = await _context.Schedules.FindAsync(uniqueId.Id);
        if (schedule == null)
        {
            throw new NotFoundException();
        }

        _context.Schedules.Remove(schedule);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Schedules
    /// </summary>
    public async Task<List<Schedule>> Schedules(ScheduleFindManyArgs findManyArgs)
    {
        var schedules = await _context
            .Schedules.Include(x => x.Teacher)
            .Include(x => x.Room)
            .Include(x => x.Student)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return schedules.ConvertAll(schedule => schedule.ToDto());
    }

    /// <summary>
    /// Get one Schedule
    /// </summary>
    public async Task<Schedule> Schedule(ScheduleWhereUniqueInput uniqueId)
    {
        var schedules = await this.Schedules(
            new ScheduleFindManyArgs { Where = new ScheduleWhereInput { Id = uniqueId.Id } }
        );
        var schedule = schedules.FirstOrDefault();
        if (schedule == null)
        {
            throw new NotFoundException();
        }

        return schedule;
    }

    /// <summary>
    /// Get a Room record for Schedule
    /// </summary>
    public async Task<Room> GetRoom(ScheduleWhereUniqueInput uniqueId)
    {
        var schedule = await _context
            .Schedules.Where(schedule => schedule.Id == uniqueId.Id)
            .Include(schedule => schedule.Room)
            .FirstOrDefaultAsync();
        if (schedule == null)
        {
            throw new NotFoundException();
        }
        return schedule.Room.ToDto();
    }

    /// <summary>
    /// Get a Student record for Schedule
    /// </summary>
    public async Task<Student> GetStudent(ScheduleWhereUniqueInput uniqueId)
    {
        var schedule = await _context
            .Schedules.Where(schedule => schedule.Id == uniqueId.Id)
            .Include(schedule => schedule.Student)
            .FirstOrDefaultAsync();
        if (schedule == null)
        {
            throw new NotFoundException();
        }
        return schedule.Student.ToDto();
    }

    /// <summary>
    /// Get a Teacher record for Schedule
    /// </summary>
    public async Task<Teacher> GetTeacher(ScheduleWhereUniqueInput uniqueId)
    {
        var schedule = await _context
            .Schedules.Where(schedule => schedule.Id == uniqueId.Id)
            .Include(schedule => schedule.Teacher)
            .FirstOrDefaultAsync();
        if (schedule == null)
        {
            throw new NotFoundException();
        }
        return schedule.Teacher.ToDto();
    }

    /// <summary>
    /// Meta data about Schedule records
    /// </summary>
    public async Task<MetadataDto> SchedulesMeta(ScheduleFindManyArgs findManyArgs)
    {
        var count = await _context.Schedules.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Update one Schedule
    /// </summary>
    public async Task UpdateSchedule(
        ScheduleWhereUniqueInput uniqueId,
        ScheduleUpdateInput updateDto
    )
    {
        var schedule = updateDto.ToModel(uniqueId);

        _context.Entry(schedule).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Schedules.Any(e => e.Id == schedule.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}
