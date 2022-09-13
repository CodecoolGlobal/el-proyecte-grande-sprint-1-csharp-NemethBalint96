using ElProyecteGrande.Dal;
using ElProyecteGrande.Models;
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
        _roomService = Substitute.For<RoomService>(new InitDatabase().CreateContext());
        _context = new InitDatabase().CreateContext();
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
        _roomService.AddRoomToBooking(3, 1);
        var expected = 3;
        var actual = _context.Bookings.Include(Booking => Booking.Room).First().Room!.Id;
        Assert.That(actual, Is.EqualTo(expected));
    }
};

