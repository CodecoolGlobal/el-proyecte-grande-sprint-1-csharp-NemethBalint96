using ElProyecteGrande.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;
using OurNonfictionBackend.Models;

namespace OurNonFictionTest;

public class InitDatabase
{
    private readonly DbContextOptions<NonfictionContext> _contextOptions;
    public InitDatabase()
    {
        _contextOptions = new DbContextOptionsBuilder<NonfictionContext>()
        .UseInMemoryDatabase("nonfiction")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        using var context = new NonfictionContext(_contextOptions);
        //var bookingJson = File.ReadAllText(@"Data\newBooking.json");
        //var bookings = JsonConvert.DeserializeObject<List<Booking>>(bookingJson);
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        var booking = new Booking
        {
            BookersName = "Kiss Bence",
            Guests = null,
            Email = "kissbence19@gmail.com",
            Room = null,
            Adults = 0,
            Children = 0,
            Infants = 0,
            Country = "Hun",
            ArrivalDate = DateTime.Now,
            DepartureDate = DateTime.Now.AddDays(1),
            Status = Status.Confirmed,
            Created = DateTime.Now,
            ModificationDate = DateTime.Now
        };
        context.Bookings.Add(booking);
        context.Accounts.Add(new Account()
        {
            Email = "admin@admin.com",
            Username = "admin",
            Password = BCrypt.Net.BCrypt.HashPassword("admin"),
            Role = "Admin"
        });

        context.SaveChanges();
    }

    public NonfictionContext CreateContext() => new(_contextOptions);
}
