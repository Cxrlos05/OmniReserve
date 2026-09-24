using MediatR;
using Microsoft.AspNetCore.Mvc;
using OmniReserve.Application.Rooms.Commands.CreateRoom;

namespace OmniReserve.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    private readonly IMediator _mediator;

    public RoomsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateRoom(
        CreateRoomCommand command,
        CancellationToken cancellationToken)
    {
        var roomId = await _mediator.Send(command, cancellationToken);

        return Ok(roomId);
    }
}