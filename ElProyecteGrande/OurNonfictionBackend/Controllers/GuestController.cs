using ElProyecteGrande.Dal;
using ElProyecteGrande.Models;
using Microsoft.AspNetCore.Mvc;
using OurNonfictionBackend.Dal;

namespace OurNonfictionBackend.Controllers;
[ApiController, Route("[controller]")]
public class GuestApiController : ControllerBase
{
    private readonly IGuestService _guestService;

    public GuestApiController(IGuestService guestService)
    {
        _guestService = guestService;
    }

    [HttpGet]
    public Task<IEnumerable<Guest>> GetAllNamedGuests()
    {
        var guests = _guestService.GetAllNamedGuests();
        return guests;
    }

    [HttpGet("{guestId}")]
    public ActionResult GetGuest(int guestId)
    {
        var guest = _guestService.GetGuest(guestId);
        if (guest is null)
            return NotFound();

        return Ok(guest);
    }

    [HttpDelete("{guestId}")]
    public async Task DeleteGuestFromBooking(int guestId)
    {
        await _guestService.DeleteGuestFromBooking(guestId);
    }

    [HttpPut("{guestId}")]
    public async Task EditGuest(int guestId, Guest guest)
    {
        await _guestService.EditGuest(guest);
    }

    [HttpPost("{bookingId}/addnew")]
    public async Task<Guest> AddNewGuestToBooking(int bookingId, Guest guest)
    {
        await _guestService.AddNewGuestToBooking(bookingId, guest);
        return await _guestService.GetLatestGuest();
    }
}
