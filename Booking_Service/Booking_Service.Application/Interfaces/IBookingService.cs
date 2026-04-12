using Booking_Service.Application.Common;
using Booking_Service.Application.DTOs.Bookings;

namespace Booking_Service.Application.Interfaces;

public interface IBookingService
{
    Task<IEnumerable<BookingDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<BookingDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<OperationResult<Guid>> CreateAsync(CreateBookingRequest request, CancellationToken cancellationToken = default);
    Task<OperationResult<bool>> UpdateAsync(Guid id, UpdateBookingRequest request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}