using ElProyecteGrande.Dal;
using ElProyecteGrande.Models;
using Microsoft.AspNetCore.Mvc;

namespace OurNonfictionBackend.Controllers;
[ApiController, Route("[controller]")]
public class GuestController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public GuestController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Guest>> GetAllNamedGuests()
    {
        return Ok(_bookingService.GetAllNamedGuests());
    }

    [HttpGet("{guestId}")]
    public ActionResult GetGuest(int guestId)
    {
        return Ok(_bookingService.GetGuest(guestId));
    }

    [HttpDelete("{guestId}")]
    public ActionResult DeleteGuestFromBooking(int guestId)
    {
        _bookingService.DeleteGuestFromBooking(guestId);
        return NoContent();
    }

    [HttpPut("{guestId}")]
    public ActionResult EditGuest(int guestId, Guest guest)
    {
        if (guestId != guest.Id)
            return BadRequest();

        var existingGuest = _bookingService.GetGuest(guestId);
        if (existingGuest is null)
            return NotFound();

        _bookingService.EditGuest(guest);
        return NoContent();
    }
}
