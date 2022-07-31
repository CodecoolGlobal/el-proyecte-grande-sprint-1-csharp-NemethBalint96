using ElProyecteGrande.Dal;
using ElProyecteGrande.Dao;
using ElProyecteGrande.Models;
using Newtonsoft.Json;

namespace ElProyecteGrande.Data;

public class Dataseed
{
    public static void SetupInMemoryRooms(RoomDaoMemory rooms)
    {
        for (var i = 1; i < 6; i++)
        {
            var room = new Room
            {
                DoorNumber = i,
                Floor = 1,
                RoomType = RoomType.Standard,
                Price = 80
            };
            rooms.Add(room);
        }
        for (var i = 1; i < 6; i++)
        {
            var room = new Room
            {
                DoorNumber = i,
                Floor = 2,
                RoomType = RoomType.Superior,
                Price = 100
            };
            rooms.Add(room);
        }
        for (var i = 1; i < 6; i++)
        {
            var room = new Room
            {
                DoorNumber = i,
                Floor = 3,
                RoomType = RoomType.Apartman,
                Price = 150
            };
            rooms.Add(room);
        }
    }

    public static void CreateBookings(IBookingService bookingService, RoomDaoMemory roomDao)
    {
        string json = File.ReadAllText(@"wwwroot\booking.json");
        List<Booking> bookings = JsonConvert.DeserializeObject<List<Booking>>(json);
        foreach (var booking in bookings)
        {
            var random = new Random();
            booking.DepartureDate = booking.ArrivalDate.AddDays(random.Next(1, 7));
            booking.Created = booking.ArrivalDate.Subtract(TimeSpan.FromDays(random.Next(1, 7)));
            booking.ModificationDate = booking.Created.AddDays(random.Next(1, 7));
            var rooms = roomDao.GetAll().ToList();
            booking.Room = rooms[random.Next(1, rooms.Count)];
            booking.Room.Bookings.Add(booking);
            booking.Guests = new List<Guest>{
                new Guest(Age.Adult)
                {
                    FullName = booking.BookersName,
                    Email = booking.Email,
                    Country = booking.Country,
                    Citizenship = booking.Country,
                },
            };
            bookingService.Add(booking);
        }
    }
}
