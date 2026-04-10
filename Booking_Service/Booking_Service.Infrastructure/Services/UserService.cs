using Booking_Service.Application.DTOs.Users;
using Booking_Service.Application.Interfaces;
using Booking_Service.Domain.Entities;
using Booking_Service.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Booking_Service.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .AsNoTracking()
            .Select(user => new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<UserDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .AsNoTracking()
            .Where(user => user.Id == id)
            .Select(user => new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Guid> CreateAsync(CreateUserRequest request, CancellationToken cancellationToken = default)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Email = request.Email
        };

        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return user.Id;
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateUserRequest request, CancellationToken cancellationToken = default)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (user is null)
            return false;

        user.Name = request.Name;
        user.Email = request.Email;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (user is null)
            return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}