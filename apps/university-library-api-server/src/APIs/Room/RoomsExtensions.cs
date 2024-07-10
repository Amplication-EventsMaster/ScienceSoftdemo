using UniversityLibraryApi.APIs.Dtos;
using UniversityLibraryApi.Infrastructure.Models;

namespace UniversityLibraryApi.APIs.Extensions;

public static class RoomsExtensions
{
    public static Room ToDto(this RoomDbModel model)
    {
        return new Room
        {
            Id = model.Id,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            RoomNumber = model.RoomNumber,
            Capacity = model.Capacity,
            Schedules = model.Schedules?.Select(x => x.Id).ToList(),
        };
    }

    public static RoomDbModel ToModel(this RoomUpdateInput updateDto, RoomWhereUniqueInput uniqueId)
    {
        var room = new RoomDbModel
        {
            Id = uniqueId.Id,
            RoomNumber = updateDto.RoomNumber,
            Capacity = updateDto.Capacity
        };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            room.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            room.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return room;
    }
}
