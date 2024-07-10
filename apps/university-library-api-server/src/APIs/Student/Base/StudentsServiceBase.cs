using Microsoft.EntityFrameworkCore;
using UniversityLibraryApi.APIs;
using UniversityLibraryApi.APIs.Common;
using UniversityLibraryApi.APIs.Dtos;
using UniversityLibraryApi.APIs.Errors;
using UniversityLibraryApi.APIs.Extensions;
using UniversityLibraryApi.Infrastructure;
using UniversityLibraryApi.Infrastructure.Models;

namespace UniversityLibraryApi.APIs;

public abstract class StudentsServiceBase : IStudentsService
{
    protected readonly UniversityLibraryApiDbContext _context;

    public StudentsServiceBase(UniversityLibraryApiDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Student
    /// </summary>
    public async Task<Student> CreateStudent(StudentCreateInput createDto)
    {
        var student = new StudentDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt,
            Name = createDto.Name,
            Email = createDto.Email,
            Department = createDto.Department
        };

        if (createDto.Id != null)
        {
            student.Id = createDto.Id;
        }
        if (createDto.Schedules != null)
        {
            student.Schedules = await _context
                .Schedules.Where(schedule =>
                    createDto.Schedules.Select(t => t.Id).Contains(schedule.Id)
                )
                .ToListAsync();
        }

        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<StudentDbModel>(student.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Student
    /// </summary>
    public async Task DeleteStudent(StudentWhereUniqueInput uniqueId)
    {
        var student = await _context.Students.FindAsync(uniqueId.Id);
        if (student == null)
        {
            throw new NotFoundException();
        }

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Students
    /// </summary>
    public async Task<List<Student>> Students(StudentFindManyArgs findManyArgs)
    {
        var students = await _context
            .Students.Include(x => x.Schedules)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return students.ConvertAll(student => student.ToDto());
    }

    /// <summary>
    /// Get one Student
    /// </summary>
    public async Task<Student> Student(StudentWhereUniqueInput uniqueId)
    {
        var students = await this.Students(
            new StudentFindManyArgs { Where = new StudentWhereInput { Id = uniqueId.Id } }
        );
        var student = students.FirstOrDefault();
        if (student == null)
        {
            throw new NotFoundException();
        }

        return student;
    }

    /// <summary>
    /// Connect multiple Schedules records to Student
    /// </summary>
    public async Task ConnectSchedules(
        StudentWhereUniqueInput uniqueId,
        ScheduleWhereUniqueInput[] schedulesId
    )
    {
        var student = await _context
            .Students.Include(x => x.Schedules)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (student == null)
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

        var schedulesToConnect = schedules.Except(student.Schedules);

        foreach (var schedule in schedulesToConnect)
        {
            student.Schedules.Add(schedule);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Schedules records from Student
    /// </summary>
    public async Task DisconnectSchedules(
        StudentWhereUniqueInput uniqueId,
        ScheduleWhereUniqueInput[] schedulesId
    )
    {
        var student = await _context
            .Students.Include(x => x.Schedules)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (student == null)
        {
            throw new NotFoundException();
        }

        var schedules = await _context
            .Schedules.Where(t => schedulesId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var schedule in schedules)
        {
            student.Schedules?.Remove(schedule);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Schedules records for Student
    /// </summary>
    public async Task<List<Schedule>> FindSchedules(
        StudentWhereUniqueInput uniqueId,
        ScheduleFindManyArgs studentFindManyArgs
    )
    {
        var schedules = await _context
            .Schedules.Where(m => m.StudentId == uniqueId.Id)
            .ApplyWhere(studentFindManyArgs.Where)
            .ApplySkip(studentFindManyArgs.Skip)
            .ApplyTake(studentFindManyArgs.Take)
            .ApplyOrderBy(studentFindManyArgs.SortBy)
            .ToListAsync();

        return schedules.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Meta data about Student records
    /// </summary>
    public async Task<MetadataDto> StudentsMeta(StudentFindManyArgs findManyArgs)
    {
        var count = await _context.Students.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Update multiple Schedules records for Student
    /// </summary>
    public async Task UpdateSchedules(
        StudentWhereUniqueInput uniqueId,
        ScheduleWhereUniqueInput[] schedulesId
    )
    {
        var student = await _context
            .Students.Include(t => t.Schedules)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (student == null)
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

        student.Schedules = schedules;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Update one Student
    /// </summary>
    public async Task UpdateStudent(StudentWhereUniqueInput uniqueId, StudentUpdateInput updateDto)
    {
        var student = updateDto.ToModel(uniqueId);

        if (updateDto.Schedules != null)
        {
            student.Schedules = await _context
                .Schedules.Where(schedule =>
                    updateDto.Schedules.Select(t => t).Contains(schedule.Id)
                )
                .ToListAsync();
        }

        _context.Entry(student).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Students.Any(e => e.Id == student.Id))
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
