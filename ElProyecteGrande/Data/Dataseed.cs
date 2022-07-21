using System.Text.Json.Serialization;
using ElProyecteGrande.Dao;
using ElProyecteGrande.Models;
using Newtonsoft.Json;

namespace ElProyecteGrande.Data
{
    public class Dataseed
    {
        public static void SetupInMemoryDatabase(BookingDaoMemory bookingDao)
        {
            bookingDao.Add(new Booking
            {
                ArrivalDate = DateTime.Now,
                DepartureDate = DateTime.Parse("2022.07.23"),
                BookersName = "Németh Bálint",
                Country = "Hungary",
                Email = "nemeth.balint1996@gmail.com",
                Adults = 4,
                Infants = 2,
                Children = 3,
                Guests = new List<Guest>{
                    new Guest(Age.Adult)
                    {
                        FullName = "Németh Bálint",
                        BirthDate = DateOnly.Parse("1996.01.01"),
                        BirthPlace = "Eger",
                        Email = "nemeth.balint1996@gmail.com",
                        Phone = "00000000",
                        Country = "Hungary",
                        City = "Füzesabony",
                        Address = "",
                        PostalCode = 3390,
                        Citizenship = "Hungary",
                    },
                    new Guest(Age.Infant)
                    {
                        BirthDate = DateOnly.Parse("2022.01.01"),
                    }
                },
                Room = new Room
                {
                    Comment = "  ",
                    Floor = 1,
                    DoorNumber = 2,
                    Price = 20
                },
                Status = Status.Confirmed,
            });
        }

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

        public static void CreateBookings(BookingDaoMemory bookingDao,RoomDaoMemory roomDao)
        {
            string json = File.ReadAllText(@"wwwroot\booking.json");
            List <Booking> bookings = JsonConvert.DeserializeObject<List<Booking>>(json);
            foreach (var booking in bookings)
            {
                var random = new Random();
                booking.DepartureDate = booking.ArrivalDate.AddDays(random.Next(1,7));
                booking.Created = booking.ArrivalDate.Subtract(TimeSpan.FromDays(random.Next(1, 7)));
                var rooms = roomDao.GetAll().ToList();
                booking.Room = rooms[random.Next(1, rooms.Count)];
                booking.Room.Bookings.Add(booking);
                bookingDao.Add(booking);
            }

        }
    }

    
}
