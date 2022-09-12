using ElProyecteGrande.Dal;
using ElProyecteGrande.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;
using OurNonfictionBackend.Models;

namespace OurNonFictionTest;

    public class RoomServiceTest
    {
        private readonly DbContextOptions<NonfictionContext> _contextOptions;
        private RoomService _roomService;
        private NonfictionContext _context;
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
            _context = CreateContext();
        }

        [Test] 
        public void GetAll_ReturnsCountOfRooms()
        {
            
            var expected = Task.Run(()=>_context.Rooms.ToListAsync()).Result.Count;
            var actual = Task.Run(() => _roomService.GetAll()).Result.Count;
            Assert.That(actual,Is.EqualTo(expected));
        }

        [Test]
        public void GetByRoomId_ReturnsTheGoodRoom()
        {
            var expected = _context.Rooms.First().Id;
            var actual = Task.Run(()=>_roomService.Get(1)).Result.Id;
            Assert.That(actual,Is.EqualTo(expected));
        }

        [Test]
        public void AddRoomToBookingReturnsTheSameRoom()
        {
            _roomService.AddRoomToBooking(3, 1);
            var expected = 3;
            var actual = _context.Bookings.Include(Booking=>Booking.Room).First().Room!.Id;
            Assert.That(actual,Is.EqualTo(expected));

        }

        NonfictionContext CreateContext() => new(_contextOptions);
    };
    
