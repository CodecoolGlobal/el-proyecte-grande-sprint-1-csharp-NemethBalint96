using ElProyecteGrande.Models;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using OurNonfictionBackend.Dal;
using OurNonfictionBackend.Models;

namespace OurNonFictionTest.Services;

public class GuestServiceTest
{
    private NonfictionContext _context;
    private GuestService _guestService;

    [SetUp]
    public void Setup()
    {
        _context = Substitute.For<InitDatabase>().CreateContext();
        _guestService = Substitute.For<GuestService>(_context);

    }

    [Test, Order(2)]
    public void GetGuest_ReturnsTheGuest()
    {
        var expected = _context.Guests.First().Id;
        var actual = Task.Run(() => _guestService.GetGuest(1)).Result.Id;
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test, Order(3)]
    public void EditGuest_UpdatesGuest()
    {
        var expected = "Miskolc";
        var toBeUpdatedGuest = _context.Guests.First();
        toBeUpdatedGuest.City = expected;
        _guestService.EditGuest(toBeUpdatedGuest);
        var actual = _guestService.GetGuest(1).Result.City;
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test, Order(1)]
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
        var actual = _context.Bookings.Include(booking => booking.Guests).First(booking => booking.Id == bookingId)
            .Guests.Last().FullName;
        var guests = _context.Bookings.Include(booking => booking.Guests).First(booking => booking.Id == bookingId)
            .Guests;
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test, Order(1)]
    public void GetLatestGuest_ReturnsGuestWithHighestId()
    {
        var expected = _context.Guests.Last().Id;
        var actual = _guestService.GetLatestGuest().Result.Id;
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
}

