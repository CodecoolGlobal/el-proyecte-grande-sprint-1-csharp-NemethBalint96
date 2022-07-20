using ElProyecteGrande.Dao;
using ElProyecteGrande.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ElProyecteGrande.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly BookingDaoMemory _bookingDaoMemory;

    public HomeController(ILogger<HomeController> logger, BookingDaoMemory booking)
    {
        _logger = logger;
        _bookingDaoMemory = booking;
    }

    public IActionResult Bookings()
    {
        
        var books = _bookingDaoMemory.GetAll();
        return View(books);
    }

    public IActionResult Guests()
    {
        var guests = _bookingDaoMemory.GetAll().SelectMany(booking => booking.Guests);
        return View(guests);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult BookingForm(int id)
    {
        if (id != 0)
        {
            var booking = _bookingDaoMemory.Get(id);
            return View(booking);
        }

        return View();
    }

    [HttpPost]
    public RedirectToActionResult AddNewBooking(Booking booking)
    {
        _bookingDaoMemory.Add(booking);
        return RedirectToAction("Bookings");
    }

    public RedirectToActionResult CancelBooking(int id)
    {
        _bookingDaoMemory.SetStatusCancelled(id);
        return RedirectToAction("Reservation", new { id });
    }

    public IActionResult Reservation(int id)
    {
        
        var booking = _bookingDaoMemory.Get(id);
        if (booking == null)
        {
            return RedirectToAction("Bookings");
        }
        return View(booking);
    }

    [HttpPost]
    public RedirectToActionResult EditBooking(Booking booking)
    {
        _bookingDaoMemory.Edit(booking);
        return RedirectToAction("Reservation", new { booking.ID });
    }
}