using ElProyecteGrande.Dal;
using ElProyecteGrande.Models;
using Microsoft.AspNetCore.Mvc;

namespace OurNonfictionBackend.Controllers;
[ApiController, Route("[controller]")]
public class GuestApiController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public GuestApiController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpGet]
    public Task<IEnumerable<Guest>> GetAllNamedGuests()
    {
        var guests = _bookingService.GetAllNamedGuests();
        return guests;
    }

    [HttpGet("{guestId}")]
    public ActionResult GetGuest(int guestId)
    {
        var guest = _bookingService.GetGuest(guestId);
        if (guest is null)
            return NotFound();

        return Ok(guest);
    }

    [HttpDelete("{guestId}")]
    public async Task DeleteGuestFromBooking(int guestId)
    {
        await _bookingService.DeleteGuestFromBooking(guestId);
    }

    [HttpPut("{guestId}")]
    public async Task EditGuest(int guestId, Guest guest)
    {
        await _bookingService.EditGuest(guest);
    }
}
