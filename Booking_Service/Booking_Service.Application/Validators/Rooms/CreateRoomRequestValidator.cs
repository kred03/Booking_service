using Booking_Service.Application.DTOs.Rooms;
using FluentValidation;

namespace Booking_Service.Application.Validators.Rooms;

public class CreateRoomRequestValidator : AbstractValidator<CreateRoomRequest>
{
    public CreateRoomRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Название комнаты обязательно.")
            .MaximumLength(100).WithMessage("Название комнаты не должно быть длиннее 100 символов.");

        RuleFor(x => x.Capacity)
            .GreaterThan(0).WithMessage("Вместимость должна быть больше 0.");
    }
}