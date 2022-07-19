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

    public IActionResult NewBooking()
    {
        return View();
    }

    [HttpPost]
    public RedirectToActionResult AddNewBooking(Booking booking)
    {
        booking.ID = _bookingDaoMemory.GetAll().Last().ID + 1;
        _bookingDaoMemory.Add(booking);
        return RedirectToAction("Bookings");
    }
    [HttpGet("delete/{id}")]
    public RedirectToActionResult  DeleteBooking(int id)
    {
        _bookingDaoMemory.Delete(id);
        return RedirectToAction("Bookings");
    }
}