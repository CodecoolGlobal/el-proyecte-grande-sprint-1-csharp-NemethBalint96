using ElProyecteGrande.Dal;
using ElProyecteGrande.Models;
using Microsoft.AspNetCore.Mvc;
using OurNonfictionBackend.Dal;

namespace OurNonfictionBackend.Controllers;
[ApiController, Route("[controller]")]
public class RoomApiController : ControllerBase
{
    private readonly IRoomService _roomService;
    private readonly IBookingDetailsService _bookingDetailsServiceService;

    public RoomApiController(IRoomService roomService, IBookingDetailsService bookingDetailsServiceService)
    {
        _roomService = roomService;
        _bookingDetailsServiceService = bookingDetailsServiceService;
    }

    [HttpGet]
    public async Task<List<Room>> GetAll()
    {
        return await  _roomService.GetAll();
    }

    [HttpGet("available/{bookingId}")]
    public async Task<List<Room>> GetAvailableRooms(long bookingId)
    {
        return await _bookingDetailsServiceService.FilterRoomsByBookingDate(bookingId);
    }

    [HttpGet("{roomId}/{bookingId}")]
    public async Task AddRoomToBooking(long roomId, long bookingId)
    {
        await _bookingDetailsServiceService.AddRoomToBooking(roomId, bookingId);
    }
}
