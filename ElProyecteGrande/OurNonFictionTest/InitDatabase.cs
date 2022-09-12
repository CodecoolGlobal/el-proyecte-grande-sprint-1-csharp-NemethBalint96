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
        var bookingJson = File.ReadAllText(@"Data\newBooking.json");
        var bookings = JsonConvert.DeserializeObject<List<Booking>>(bookingJson);
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        context.Bookings.AddRange(bookings);
        context.Accounts.Add(new Account()
        {
            Email = "admin@admin.com",
            Username = "admin",
            Password = BCrypt.Net.BCrypt.HashPassword("admin"),
            Role =
                "Admin"
        });

        context.SaveChanges();
    }

    public NonfictionContext CreateContext() => new(_contextOptions);
}

