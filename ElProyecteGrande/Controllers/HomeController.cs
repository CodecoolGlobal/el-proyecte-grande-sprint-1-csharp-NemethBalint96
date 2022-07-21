using ElProyecteGrande.Dao;
using ElProyecteGrande.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ElProyecteGrande.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly BookingDaoMemory _bookingDaoMemory;
    private readonly RoomDaoMemory _roomDaoMemory;

    public HomeController(ILogger<HomeController> logger, BookingDaoMemory booking, RoomDaoMemory room)
    {
        _logger = logger;
        _bookingDaoMemory = booking;
        _roomDaoMemory = room;
    }

    public IActionResult Bookings()
    {
        var books = _bookingDaoMemory.GetAll();
        return View(books);
    }

    public IActionResult Guests()
    {
        var guests = _bookingDaoMemory.GetAll();
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
        var booking = _bookingDaoMemory.Get(id);
        return View(booking);
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
        HttpContext.Session.SetString("BookingId", $"{booking.ID}");
        return RedirectToAction("SelectRoom");
    }

    [HttpGet]
    public IActionResult DeleteGuestFromBooking(int bookingId, int guestId)
    {
        _bookingDaoMemory.DeleteGuestFromBooking(bookingId, guestId);
        var booking = _bookingDaoMemory.Get(bookingId);
        return View("Reservation", booking);
    }

    public IActionResult EditGuest(int id)
    {
        var guest = _bookingDaoMemory.GetGuest(id);
        return View(guest);
    }

    [HttpPost]
    public IActionResult EditGuest(Guest guest)
    {
        var booking = _bookingDaoMemory.EditGuestReturnBooking(guest);
        return View("Reservation", booking);
    }

    public IActionResult Rooms()
    {
        var rooms = _roomDaoMemory.GetAll();
        return View(rooms);
    }

    [HttpGet]
    public IActionResult SelectRoom()
    {
        var id = HttpContext.Session.GetString("BookingId");
        var booking = _bookingDaoMemory.Get(int.Parse(id));
        var rooms = _roomDaoMemory.GetAvailable(booking);
        rooms = rooms.OrderBy(room => room.Floor).ThenBy(room => room.DoorNumber);
        return View(rooms);
    }

    [HttpPost]
    public IActionResult Select()
    {
        var id = int.Parse(HttpContext.Session.GetString("BookingId"));
        var roomId = int.Parse(Request.Form["rooms"]);
        var room = _roomDaoMemory.Get(roomId);
        var reservation = _bookingDaoMemory.Get(id);
        _roomDaoMemory.ChangeRoom(reservation);

        var booking = _bookingDaoMemory.AddRoomToBooking(id, room);
        return View("Reservation", booking);
    }
}
