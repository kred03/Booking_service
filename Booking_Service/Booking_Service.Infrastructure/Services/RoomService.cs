using Booking_Service.Application.DTOs.Rooms;
using Booking_Service.Application.Interfaces;
using Booking_Service.Domain.Entities;
using Booking_Service.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Booking_Service.Infrastructure.Services;

public class RoomService : IRoomService
{
    private readonly ApplicationDbContext _context;

    public RoomService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<RoomDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Rooms
            .AsNoTracking()
            .Select(room => new RoomDto
            {
                Id = room.Id,
                Name = room.Name,
                Capacity = room.Capacity
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<RoomDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Rooms
            .AsNoTracking()
            .Where(room => room.Id == id)
            .Select(room => new RoomDto
            {
                Id = room.Id,
                Name = room.Name,
                Capacity = room.Capacity
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Guid> CreateAsync(CreateRoomRequest request, CancellationToken cancellationToken = default)
    {
        var room = new Room
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Capacity = request.Capacity
        };

        await _context.Rooms.AddAsync(room, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return room.Id;
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateRoomRequest request, CancellationToken cancellationToken = default)
    {
        var room = await _context.Rooms.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (room is null)
            return false;

        room.Name = request.Name;
        room.Capacity = request.Capacity;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var room = await _context.Rooms.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (room is null)
            return false;

        _context.Rooms.Remove(room);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}