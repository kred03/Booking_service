using Booking_Service.Application.DTOs.Bookings;
using Booking_Service.Application.DTOs.Rooms;
using Booking_Service.Application.DTOs.Users;
using Booking_Service.Application.Validators.Bookings;
using Booking_Service.Application.Validators.Rooms;
using Booking_Service.Application.Validators.Users;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Booking_Service.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateRoomRequest>, CreateRoomRequestValidator>();
        services.AddScoped<IValidator<UpdateRoomRequest>, UpdateRoomRequestValidator>();

        services.AddScoped<IValidator<CreateUserRequest>, CreateUserRequestValidator>();
        services.AddScoped<IValidator<UpdateUserRequest>, UpdateUserRequestValidator>();

        services.AddScoped<IValidator<CreateBookingRequest>, CreateBookingRequestValidator>();
        services.AddScoped<IValidator<UpdateBookingRequest>, UpdateBookingRequestValidator>();

        return services;
    }
}