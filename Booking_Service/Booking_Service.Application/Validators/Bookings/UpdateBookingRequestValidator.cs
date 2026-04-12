using Booking_Service.Application.DTOs.Bookings;
using FluentValidation;

namespace Booking_Service.Application.Validators.Bookings;

public class UpdateBookingRequestValidator : AbstractValidator<UpdateBookingRequest>
{
    public UpdateBookingRequestValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId обязателен.");

        RuleFor(x => x.RoomId)
            .NotEmpty().WithMessage("RoomId обязателен.");

        RuleFor(x => x.StartAt)
            .LessThan(x => x.EndAt)
            .WithMessage("Дата начала должна быть раньше даты окончания.");

        RuleFor(x => x.EndAt)
            .GreaterThan(x => x.StartAt)
            .WithMessage("Дата окончания должна быть позже даты начала.");
    }
}