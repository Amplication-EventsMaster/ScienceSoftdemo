using Microsoft.EntityFrameworkCore;
using UniversityLibraryApi.APIs;
using UniversityLibraryApi.APIs.Common;
using UniversityLibraryApi.APIs.Dtos;
using UniversityLibraryApi.APIs.Errors;
using UniversityLibraryApi.APIs.Extensions;
using UniversityLibraryApi.Infrastructure;
using UniversityLibraryApi.Infrastructure.Models;

namespace UniversityLibraryApi.APIs;

public abstract class TeachersServiceBase : ITeachersService
{
    protected readonly UniversityLibraryApiDbContext _context;

    public TeachersServiceBase(UniversityLibraryApiDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Teacher
    /// </summary>
    public async Task<Teacher> CreateTeacher(TeacherCreateInput createDto)
    {
        var teacher = new TeacherDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt,
            Name = createDto.Name,
            Email = createDto.Email,
            Department = createDto.Department
        };

        if (createDto.Id != null)
        {
            teacher.Id = createDto.Id;
        }
        if (createDto.Schedules != null)
        {
            teacher.Schedules = await _context
                .Schedules.Where(schedule =>
                    createDto.Schedules.Select(t => t.Id).Contains(schedule.Id)
                )
                .ToListAsync();
        }

        _context.Teachers.Add(teacher);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<TeacherDbModel>(teacher.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Teacher
    /// </summary>
    public async Task DeleteTeacher(TeacherWhereUniqueInput uniqueId)
    {
        var teacher = await _context.Teachers.FindAsync(uniqueId.Id);
        if (teacher == null)
        {
            throw new NotFoundException();
        }

        _context.Teachers.Remove(teacher);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Teachers
    /// </summary>
    public async Task<List<Teacher>> Teachers(TeacherFindManyArgs findManyArgs)
    {
        var teachers = await _context
            .Teachers.Include(x => x.Schedules)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return teachers.ConvertAll(teacher => teacher.ToDto());
    }

    /// <summary>
    /// Get one Teacher
    /// </summary>
    public async Task<Teacher> Teacher(TeacherWhereUniqueInput uniqueId)
    {
        var teachers = await this.Teachers(
            new TeacherFindManyArgs { Where = new TeacherWhereInput { Id = uniqueId.Id } }
        );
        var teacher = teachers.FirstOrDefault();
        if (teacher == null)
        {
            throw new NotFoundException();
        }

        return teacher;
    }

    /// <summary>
    /// Connect multiple Schedules records to Teacher
    /// </summary>
    public async Task ConnectSchedules(
        TeacherWhereUniqueInput uniqueId,
        ScheduleWhereUniqueInput[] schedulesId
    )
    {
        var teacher = await _context
            .Teachers.Include(x => x.Schedules)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (teacher == null)
        {
            throw new NotFoundException();
        }

        var schedules = await _context
            .Schedules.Where(t => schedulesId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (schedules.Count == 0)
        {
            throw new NotFoundException();
        }

        var schedulesToConnect = schedules.Except(teacher.Schedules);

        foreach (var schedule in schedulesToConnect)
        {
            teacher.Schedules.Add(schedule);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Schedules records from Teacher
    /// </summary>
    public async Task DisconnectSchedules(
        TeacherWhereUniqueInput uniqueId,
        ScheduleWhereUniqueInput[] schedulesId
    )
    {
        var teacher = await _context
            .Teachers.Include(x => x.Schedules)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (teacher == null)
        {
            throw new NotFoundException();
        }

        var schedules = await _context
            .Schedules.Where(t => schedulesId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var schedule in schedules)
        {
            teacher.Schedules?.Remove(schedule);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Schedules records for Teacher
    /// </summary>
    public async Task<List<Schedule>> FindSchedules(
        TeacherWhereUniqueInput uniqueId,
        ScheduleFindManyArgs teacherFindManyArgs
    )
    {
        var schedules = await _context
            .Schedules.Where(m => m.TeacherId == uniqueId.Id)
            .ApplyWhere(teacherFindManyArgs.Where)
            .ApplySkip(teacherFindManyArgs.Skip)
            .ApplyTake(teacherFindManyArgs.Take)
            .ApplyOrderBy(teacherFindManyArgs.SortBy)
            .ToListAsync();

        return schedules.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Meta data about Teacher records
    /// </summary>
    public async Task<MetadataDto> TeachersMeta(TeacherFindManyArgs findManyArgs)
    {
        var count = await _context.Teachers.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Update multiple Schedules records for Teacher
    /// </summary>
    public async Task UpdateSchedules(
        TeacherWhereUniqueInput uniqueId,
        ScheduleWhereUniqueInput[] schedulesId
    )
    {
        var teacher = await _context
            .Teachers.Include(t => t.Schedules)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (teacher == null)
        {
            throw new NotFoundException();
        }

        var schedules = await _context
            .Schedules.Where(a => schedulesId.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (schedules.Count == 0)
        {
            throw new NotFoundException();
        }

        teacher.Schedules = schedules;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Update one Teacher
    /// </summary>
    public async Task UpdateTeacher(TeacherWhereUniqueInput uniqueId, TeacherUpdateInput updateDto)
    {
        var teacher = updateDto.ToModel(uniqueId);

        if (updateDto.Schedules != null)
        {
            teacher.Schedules = await _context
                .Schedules.Where(schedule =>
                    updateDto.Schedules.Select(t => t).Contains(schedule.Id)
                )
                .ToListAsync();
        }

        _context.Entry(teacher).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Teachers.Any(e => e.Id == teacher.Id))
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
