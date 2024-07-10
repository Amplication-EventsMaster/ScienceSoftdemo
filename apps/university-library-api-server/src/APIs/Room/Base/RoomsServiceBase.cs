using Microsoft.EntityFrameworkCore;
using UniversityLibraryApi.APIs;
using UniversityLibraryApi.APIs.Common;
using UniversityLibraryApi.APIs.Dtos;
using UniversityLibraryApi.APIs.Errors;
using UniversityLibraryApi.APIs.Extensions;
using UniversityLibraryApi.Infrastructure;
using UniversityLibraryApi.Infrastructure.Models;

namespace UniversityLibraryApi.APIs;

public abstract class RoomsServiceBase : IRoomsService
{
    protected readonly UniversityLibraryApiDbContext _context;

    public RoomsServiceBase(UniversityLibraryApiDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Room
    /// </summary>
    public async Task<Room> CreateRoom(RoomCreateInput createDto)
    {
        var room = new RoomDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt,
            RoomNumber = createDto.RoomNumber,
            Capacity = createDto.Capacity
        };

        if (createDto.Id != null)
        {
            room.Id = createDto.Id;
        }
        if (createDto.Schedules != null)
        {
            room.Schedules = await _context
                .Schedules.Where(schedule =>
                    createDto.Schedules.Select(t => t.Id).Contains(schedule.Id)
                )
                .ToListAsync();
        }

        _context.Rooms.Add(room);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<RoomDbModel>(room.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Room
    /// </summary>
    public async Task DeleteRoom(RoomWhereUniqueInput uniqueId)
    {
        var room = await _context.Rooms.FindAsync(uniqueId.Id);
        if (room == null)
        {
            throw new NotFoundException();
        }

        _context.Rooms.Remove(room);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Rooms
    /// </summary>
    public async Task<List<Room>> Rooms(RoomFindManyArgs findManyArgs)
    {
        var rooms = await _context
            .Rooms.Include(x => x.Schedules)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return rooms.ConvertAll(room => room.ToDto());
    }

    /// <summary>
    /// Get one Room
    /// </summary>
    public async Task<Room> Room(RoomWhereUniqueInput uniqueId)
    {
        var rooms = await this.Rooms(
            new RoomFindManyArgs { Where = new RoomWhereInput { Id = uniqueId.Id } }
        );
        var room = rooms.FirstOrDefault();
        if (room == null)
        {
            throw new NotFoundException();
        }

        return room;
    }

    /// <summary>
    /// Connect multiple Schedules records to Room
    /// </summary>
    public async Task ConnectSchedules(
        RoomWhereUniqueInput uniqueId,
        ScheduleWhereUniqueInput[] schedulesId
    )
    {
        var room = await _context
            .Rooms.Include(x => x.Schedules)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (room == null)
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

        var schedulesToConnect = schedules.Except(room.Schedules);

        foreach (var schedule in schedulesToConnect)
        {
            room.Schedules.Add(schedule);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Schedules records from Room
    /// </summary>
    public async Task DisconnectSchedules(
        RoomWhereUniqueInput uniqueId,
        ScheduleWhereUniqueInput[] schedulesId
    )
    {
        var room = await _context
            .Rooms.Include(x => x.Schedules)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (room == null)
        {
            throw new NotFoundException();
        }

        var schedules = await _context
            .Schedules.Where(t => schedulesId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var schedule in schedules)
        {
            room.Schedules?.Remove(schedule);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Schedules records for Room
    /// </summary>
    public async Task<List<Schedule>> FindSchedules(
        RoomWhereUniqueInput uniqueId,
        ScheduleFindManyArgs roomFindManyArgs
    )
    {
        var schedules = await _context
            .Schedules.Where(m => m.RoomId == uniqueId.Id)
            .ApplyWhere(roomFindManyArgs.Where)
            .ApplySkip(roomFindManyArgs.Skip)
            .ApplyTake(roomFindManyArgs.Take)
            .ApplyOrderBy(roomFindManyArgs.SortBy)
            .ToListAsync();

        return schedules.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Meta data about Room records
    /// </summary>
    public async Task<MetadataDto> RoomsMeta(RoomFindManyArgs findManyArgs)
    {
        var count = await _context.Rooms.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Update multiple Schedules records for Room
    /// </summary>
    public async Task UpdateSchedules(
        RoomWhereUniqueInput uniqueId,
        ScheduleWhereUniqueInput[] schedulesId
    )
    {
        var room = await _context
            .Rooms.Include(t => t.Schedules)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (room == null)
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

        room.Schedules = schedules;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Update one Room
    /// </summary>
    public async Task UpdateRoom(RoomWhereUniqueInput uniqueId, RoomUpdateInput updateDto)
    {
        var room = updateDto.ToModel(uniqueId);

        if (updateDto.Schedules != null)
        {
            room.Schedules = await _context
                .Schedules.Where(schedule =>
                    updateDto.Schedules.Select(t => t).Contains(schedule.Id)
                )
                .ToListAsync();
        }

        _context.Entry(room).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Rooms.Any(e => e.Id == room.Id))
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
