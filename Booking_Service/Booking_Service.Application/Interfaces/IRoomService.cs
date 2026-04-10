using Booking_Service.Application.DTOs.Rooms;

namespace Booking_Service.Application.Interfaces;

public interface IRoomService
{
    Task<IEnumerable<RoomDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<RoomDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Guid> CreateAsync(CreateRoomRequest request, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Guid id, UpdateRoomRequest request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}