using ElProyecteGrande.Dal;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using OurNonfictionBackend.Models;

namespace OurNonFictionTest;

public class RoomServiceTest
{
    private RoomService _roomService;
    private NonfictionContext _context;

    [SetUp]
    public void Setup()
    {
        _context = Substitute.For<InitDatabase>().CreateContext();
        _roomService = Substitute.For<RoomService>(_context);

    }

    [Test]
    public void GetAll_ReturnsCountOfRooms()
    {
        var expected = Task.Run(() => _context.Rooms.ToListAsync()).Result.Count;
        var actual = Task.Run(() => _roomService.GetAll()).Result.Count;
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void GetByRoomId_ReturnsTheGoodRoom()
    {
        var expected = _context.Rooms.First().Id;
        var actual = Task.Run(() => _roomService.Get(1)).Result.Id;
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void AddRoomToBookingReturnsTheSameRoom()
    {
        _roomService.AddRoomToBooking(1, 1);
        var expected = 1;
        var actual = _context.Bookings.Include(Booking => Booking.Room).First().Room!.Id;
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public async Task FilterRoomsByBookingDate_DoNotReturnsNotAvailableRoom()
    {
        const int roomId = 1;
        const int bookingId = 1;
        await _roomService.AddRoomToBooking(roomId, bookingId);
        var room = await _roomService.Get(roomId);
        var availableRooms = await _roomService.FilterRoomsByBookingDate(bookingId);
        Assert.That(availableRooms, Is.Not.Contains(room));
    }
}
