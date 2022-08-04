using ElProyecteGrande.Dal;
using ElProyecteGrande.Models;
using Microsoft.AspNetCore.Mvc;

namespace OurNonfictionBackend.Controllers;
[ApiController, Route("[controller]")]
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Booking>> GetAll()
    {
        return Ok(_bookingService.GetAll());
    }

    [HttpGet("{bookingId}")]
    public ActionResult<Booking> GetBooking(int bookingId)
    {
        var booking = _bookingService.Get(bookingId);
        if (booking is null)
            return NotFound();

        return Ok(booking);
    }

    [HttpPost]
    public ActionResult AddNewBooking(Booking booking)
    {
        _bookingService.Add(booking);
        return CreatedAtAction(nameof(AddNewBooking), new { id = booking.Id }, booking);
    }

    [HttpPut("{bookingId}")]
    public ActionResult EditBooking(int bookingId, Booking booking)
    {
        if (bookingId != booking.Id)
            return BadRequest();

        var existingBooking = _bookingService.Get(bookingId);
        if (existingBooking is null)
            return NotFound();

        _bookingService.Update(booking);
        return NoContent();
    }

    [HttpDelete("{bookingId}")]
    public ActionResult SetStatusToCancelled(int bookingId)
    {
        var booking = _bookingService.Get(bookingId);
        if (booking is null)
            return NotFound();

        _bookingService.SetStatusCancelled(bookingId);
        return NoContent();
    }

    [HttpPost("{bookingId}/addnew")]
    public ActionResult AddNewGuestToBooking(int bookingId, Guest guest)
    {
        _bookingService.AddNewGuestToBooking(bookingId, guest);
        return CreatedAtAction(nameof(AddNewGuestToBooking),new{id=guest.Id},guest);
    }
}
