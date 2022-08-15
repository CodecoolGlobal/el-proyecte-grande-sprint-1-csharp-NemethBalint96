using ElProyecteGrande.Dal;
using ElProyecteGrande.Models;
using Microsoft.AspNetCore.Mvc;
using OurNonfictionBackend.Dal;

namespace OurNonfictionBackend.Controllers;
[ApiController, Route("[controller]")]
public class RoomApiController : ControllerBase
{
    private readonly IRoomService _roomService;

    public RoomApiController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    [HttpGet]
    public async Task<List<Room>> GetAll()
    {
        return await  _roomService.GetAll();
    }

    [HttpGet("available/{bookingId}")]
    public async Task<List<Room>> GetAvailableRooms(long bookingId)
    {
        return await _roomService.FilterRoomsByBookingDate(bookingId);
    }

    [HttpPut("{roomId}/{bookingId}")]
    public async Task AddRoomToBooking(long roomId, long bookingId)
    {
        await _roomService.AddRoomToBooking(roomId, bookingId);
    }
}
