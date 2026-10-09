using FluentValidation;

namespace OmniReserve.Application.Rooms.Commands.CreateRoom;

public class CreateRoomCommandValidator
    : AbstractValidator<CreateRoomCommand>
{
    public CreateRoomCommandValidator()
    {
        RuleFor(x => x.RoomNumber)
            .NotEmpty()
                .WithMessage("El número de habitación es obligatorio.")
            .MaximumLength(5)
                .WithMessage("El número de habitación no puede superar los 5 caracteres.");

        RuleFor(x => x.PricePerNight)
            .GreaterThan(0)
                .WithMessage("El precio debe ser mayor a cero.");
    }
}