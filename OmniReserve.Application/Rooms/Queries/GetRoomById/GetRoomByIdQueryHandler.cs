using MediatR;

namespace OmniReserve.Application.Rooms.Queries.GetRoomById;

public class GetRoomByIdQueryHandler : IRequestHandler<GetRoomByIdQuery, RoomResponseDto>
{
    public Task<RoomResponseDto> Handle(
        GetRoomByIdQuery request,
        CancellationToken cancellationToken)
    {
        var room = new RoomResponseDto
        {
            Id = request.RoomId,
            RoomNumber = "101",
            Type = "Single",
            Price = 1000m,
            IsAvailable = true
        };

        return Task.FromResult(room);
    }
}