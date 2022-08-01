using ElProyecteGrande.Dal;
using ElProyecteGrande.Models;
using Microsoft.AspNetCore.Mvc;
using OurNonfictionBackend.Dal;

namespace OurNonfictionBackend.Controllers;
[ApiController, Route("[controller]")]
public class RoomController : ControllerBase
{
    private readonly IRoomService _roomService;
    private readonly IBookingDetailsService _bookingDetailsServiceService;

    public RoomController(IRoomService roomService, IBookingDetailsService bookingDetailsServiceService)
    {
        _roomService = roomService;
        _bookingDetailsServiceService = bookingDetailsServiceService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Room>> GetAll()
    {
        return Ok(_roomService.GetAll());
    }

    [HttpGet("available/{bookingId}")]
    public ActionResult<IEnumerable<Room>> GetAvailableRooms(int bookingId)
    {
        return NotFound();
    }
}
