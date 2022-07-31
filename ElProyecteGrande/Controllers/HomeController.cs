using ElProyecteGrande.Dal;
using ElProyecteGrande.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ElProyecteGrande.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IBookingService _bookingService;
    private readonly IRoomService _roomService;

    public HomeController(ILogger<HomeController> logger, IBookingService bookingService, IRoomService roomService)
    {
        _logger = logger;
        _bookingService = bookingService;
        _roomService = roomService;
    }

    public IActionResult Bookings()
    {
        var books = _bookingService.GetAll();
        return View(books);
    }

    public IActionResult Guests()
    {
        var guests = _bookingService.GetAll();
        return View(guests);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult BookingForm(int id)
    {
        if (id == 0) return View();
        var booking = _bookingService.Get(id);
        return View(booking);
    }

    [HttpPost]
    public RedirectToActionResult AddNewBooking(Booking booking)
    {
        _bookingService.Add(booking);
        HttpContext.Session.SetString("BookingId", $"{booking.Id}");
        return RedirectToAction("SelectRoom");
    }

    public RedirectToActionResult CancelBooking(int id)
    {
        _bookingService.SetStatusCancelled(id);
        return RedirectToAction("Reservation", new { id });
    }

    public IActionResult Reservation(int id)
    {
        var booking = _bookingService.Get(id);
        if (booking is null)
        {
            return RedirectToAction("Bookings");
        }
        return View(booking);
    }

    [HttpPost]
    public RedirectToActionResult EditBooking(Booking booking)
    {
        _bookingService.Update(booking);
        HttpContext.Session.SetString("BookingId", $"{booking.Id}");
        return RedirectToAction("SelectRoom");
    }

    [HttpGet]
    public IActionResult DeleteGuestFromBooking(int bookingId, int guestId)
    {
        _bookingService.DeleteGuestFromBooking(bookingId, guestId);
        var booking = _bookingService.Get(bookingId);
        return View("Reservation", booking);
    }

    public IActionResult EditGuest(int id)
    {
        var guest = _bookingService.GetGuest(id);
        return View(guest);
    }

    [HttpPost]
    public IActionResult EditGuest(Guest guest)
    {
        var booking = _bookingService.EditGuestReturnBooking(guest);
        return View("Reservation", booking);
    }

    public IActionResult Rooms()
    {
        var rooms = _roomService.GetAll();
        return View(rooms);
    }

    [HttpGet]
    public IActionResult SelectRoom()
    {
        var bookingId = int.Parse(HttpContext.Session.GetString("BookingId"));
        var rooms = _bookingService.FilterRoomsByBookingDate(bookingId, _roomService.GetAll());
        rooms = rooms.OrderBy(room => room.Floor).ThenBy(room => room.DoorNumber);
        return View(rooms);
    }

    [HttpPost]
    public IActionResult Select()
    {
        var id = int.Parse(HttpContext.Session.GetString("BookingId"));
        var roomId = int.Parse(Request.Form["rooms"]);
        var room = _roomService.Get(roomId);

        var booking = _bookingService.AddRoomToBooking(id, room);
        return View("Reservation", booking);
    }
}
