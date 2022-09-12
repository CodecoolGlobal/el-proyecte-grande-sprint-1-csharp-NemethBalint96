using ElProyecteGrande.Dal;
using ElProyecteGrande.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;
using NSubstitute;
using OurNonfictionBackend.Dal;
using OurNonfictionBackend.Models;

namespace OurNonFictionTest;

public class RoomServiceTest
{
    private readonly DbContextOptions<NonfictionContext> _contextOptions;
    private RoomService _roomService;
    private NonfictionContext _context;
    private GuestService _guestService;
    public RoomServiceTest()
    {
        _contextOptions = new DbContextOptionsBuilder<NonfictionContext>()
            .UseInMemoryDatabase("nonfiction")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        using var context = new NonfictionContext(_contextOptions);
        var bookingJson = File.ReadAllText(@"Data\newBooking.json");
        var bookings = JsonConvert.DeserializeObject<List<Booking>>(bookingJson);
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        context.Bookings.AddRange(bookings);
        context.Accounts.Add(new Account() { Email = "admin@admin.com", Username = "admin", Password = BCrypt.Net.BCrypt.HashPassword("admin"), Role = "Admin" });
        context.SaveChanges();
    }

    [SetUp]
    public void Setup()
    {
        _roomService = Substitute.For<RoomService>(CreateContext());
        _guestService = Substitute.For<GuestService>(CreateContext());
        _context = CreateContext();
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

    [Test, Order(2)]
    public void GetGuest_ReturnsTheGuest()
    {
        var context = CreateContext();
        var expected = context.Guests.First().Id;
        var actual = Task.Run(() => _guestService.GetGuest(1)).Result.Id;
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test, Order(3)]
    public void EditGuest_UpdatesGuest()
    {
        var context = CreateContext();
        var expected = "Miskolc";
        var toBeUpdatedGuest = context.Guests.First();
        toBeUpdatedGuest.City = expected;
        _guestService.EditGuest(toBeUpdatedGuest);
        var actual = _guestService.GetGuest(1).Result.City;
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void AddNewGuestToBooking_AddNewGuestToBooking()
    {
        var bookingId = 1;
        var newGuest = new Guest(Age.Adult)
        {
            FullName = "Kiss Bence",
            Address = "",
            PostalCode = null,
            BirthDate = DateTime.Now,
            BirthPlace = "Miskolc",
            Citizenship = "Hun",
            City = "Haha",
            Comment = "",
            Country = "Hun",
            Email = "kiss@kiss.com",
            Phone = "",
        };
        _guestService.AddNewGuestToBooking(1, newGuest);
        var expected = newGuest.FullName;
        var actual = _context.Bookings.Include(booking => booking.Guests).First().Guests.Last().FullName;
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void GetLatestGuest_ReturnsGuestWithHighestId()
    {
        var expected = _context.Guests.Last().FullName;
        var actual = _guestService.GetLatestGuest().Result.FullName;
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void GetAllNamedGuests_ReturnsGuestWhosNameIsNotAccompanyingGuest()
    {
        var expected = _context.Guests.Where(guest => guest.FullName != "Accompanying Guest").ToList().Count;
        var actual = _guestService.GetAllNamedGuests().Result.ToList().Count;
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public async Task DeleteGuestFromBookingDecreaseTheNumberOfGuestsInBooking()
    {
        var expected = _context.Bookings.Include(booking => booking.Guests).First().Guests.Count - 1;
        await _guestService.DeleteGuestFromBooking(1);
        _context.ChangeTracker.Clear();
        var actual = _context.Bookings.Include(booking => booking.Guests).First().Guests.Count;
        Assert.That(actual, Is.EqualTo(expected));
    }
    NonfictionContext CreateContext() => new(_contextOptions);
};

