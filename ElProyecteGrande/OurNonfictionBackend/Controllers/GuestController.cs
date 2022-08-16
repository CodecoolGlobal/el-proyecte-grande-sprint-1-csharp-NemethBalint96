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
    public ActionResult<IEnumerable<Guest>> GetAllNamedGuests()
    {
        var guests = _bookingService.GetAllNamedGuests();
        return Ok(guests);
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
    public ActionResult DeleteGuestFromBooking(int guestId)
    {
        var isDeleted = _bookingService.DeleteGuestFromBooking(guestId);
        if (isDeleted)
            return NoContent();

        return NotFound();
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
