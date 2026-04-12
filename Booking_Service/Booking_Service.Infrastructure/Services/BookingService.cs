using Booking_Service.Application.Common;
using Booking_Service.Application.DTOs.Bookings;
using Booking_Service.Application.Interfaces;
using Booking_Service.Domain.Entities;
using Booking_Service.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Booking_Service.Infrastructure.Services;

public class BookingService : IBookingService
{
    private readonly ApplicationDbContext _context;

    public BookingService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BookingDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Bookings
            .AsNoTracking()
            .Include(x => x.User)
            .Include(x => x.Room)
            .Select(x => new BookingDto
            {
                Id = x.Id,
                UserId = x.UserId,
                UserName = x.User.Name,
                RoomId = x.RoomId,
                RoomName = x.Room.Name,
                StartAt = x.StartAt,
                EndAt = x.EndAt
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<BookingDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Bookings
            .AsNoTracking()
            .Include(x => x.User)
            .Include(x => x.Room)
            .Where(x => x.Id == id)
            .Select(x => new BookingDto
            {
                Id = x.Id,
                UserId = x.UserId,
                UserName = x.User.Name,
                RoomId = x.RoomId,
                RoomName = x.Room.Name,
                StartAt = x.StartAt,
                EndAt = x.EndAt
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<OperationResult<Guid>> CreateAsync(CreateBookingRequest request, CancellationToken cancellationToken = default)
    {
        var validationResult = await ValidateBookingRequestAsync(
            request.UserId,
            request.RoomId,
            request.StartAt,
            request.EndAt,
            null,
            cancellationToken);

        if (!validationResult.IsSuccess)
            return OperationResult<Guid>.Failure(validationResult.Error!);

        var booking = new Booking
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            RoomId = request.RoomId,
            StartAt = request.StartAt,
            EndAt = request.EndAt
        };

        await _context.Bookings.AddAsync(booking, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return OperationResult<Guid>.Success(booking.Id);
    }

    public async Task<OperationResult<bool>> UpdateAsync(Guid id, UpdateBookingRequest request, CancellationToken cancellationToken = default)
    {
        var booking = await _context.Bookings.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (booking is null)
            return OperationResult<bool>.Failure("Бронь не найдена.");

        var validationResult = await ValidateBookingRequestAsync(
            request.UserId,
            request.RoomId,
            request.StartAt,
            request.EndAt,
            id,
            cancellationToken);

        if (!validationResult.IsSuccess)
            return OperationResult<bool>.Failure(validationResult.Error!);

        booking.UserId = request.UserId;
        booking.RoomId = request.RoomId;
        booking.StartAt = request.StartAt;
        booking.EndAt = request.EndAt;

        await _context.SaveChangesAsync(cancellationToken);

        return OperationResult<bool>.Success(true);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var booking = await _context.Bookings.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (booking is null)
            return false;

        _context.Bookings.Remove(booking);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    private async Task<OperationResult<bool>> ValidateBookingRequestAsync(
        Guid userId,
        Guid roomId,
        DateTime startAt,
        DateTime endAt,
        Guid? currentBookingId,
        CancellationToken cancellationToken)
    {
        if (endAt <= startAt)
            return OperationResult<bool>.Failure("Дата окончания должна быть позже даты начала.");

        var userExists = await _context.Users.AnyAsync(x => x.Id == userId, cancellationToken);
        if (!userExists)
            return OperationResult<bool>.Failure("Пользователь не найден.");

        var roomExists = await _context.Rooms.AnyAsync(x => x.Id == roomId, cancellationToken);
        if (!roomExists)
            return OperationResult<bool>.Failure("Комната не найдена.");

        var hasOverlap = await _context.Bookings.AnyAsync(x =>
            x.RoomId == roomId &&
            x.Id != currentBookingId &&
            x.StartAt < endAt &&
            x.EndAt > startAt,
            cancellationToken);

        if (hasOverlap)
            return OperationResult<bool>.Failure("Комната уже занята в указанный промежуток времени.");

        return OperationResult<bool>.Success(true);
    }
}