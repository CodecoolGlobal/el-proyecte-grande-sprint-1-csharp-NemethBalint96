using ElProyecteGrande.Dal;
using ElProyecteGrande.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OurNonfictionBackend.Controllers;
[Authorize]
[ApiController, Route("[controller]")]
public class BookingApiController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingApiController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }
    
    [HttpGet]
    public async Task<List<Booking>> GetAll()
    {
        return await _bookingService.GetAll();
    }

    [HttpGet("{bookingId}")]
    public async Task<Booking> GetBooking(int bookingId)
    {
        var booking = await _bookingService.Get(bookingId);
        return booking;
    }

    [HttpPost]
    public async Task<Booking> AddNewBooking(Booking booking)
    {
        await _bookingService.Add(booking);
        return await _bookingService.GetLatestBooking();
    }

    [HttpPut("{bookingId}")]
    public async Task EditBooking(Booking booking, long bookingId)
    {
        await _bookingService.Update(booking, bookingId);
    }

    [HttpDelete("{bookingId}")]
    public async Task SetStatusToCancelled(long bookingId)
    {
        await _bookingService.SetStatusCancelled(bookingId);
    }
}
