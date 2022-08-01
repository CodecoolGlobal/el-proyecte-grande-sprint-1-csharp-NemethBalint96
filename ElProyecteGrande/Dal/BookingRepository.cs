using ElProyecteGrande.Models;
using Newtonsoft.Json;

namespace ElProyecteGrande.Dal;
public class BookingRepository : IRepository<Booking>
{
    private readonly List<Booking> _bookings;
    private readonly IRepository<Room> _roomRepository;
    public BookingRepository(IRepository<Room> roomRepository)
    {
        _bookings = new List<Booking>();
        _roomRepository = roomRepository;
        CreateBookings();
    }

    private void CreateBookings()
    {
        string json = File.ReadAllText(@"Data\booking.json");
        List<Booking> bookings = JsonConvert.DeserializeObject<List<Booking>>(json);
        foreach (var booking in bookings)
        {
            var random = new Random();
            booking.DepartureDate = booking.ArrivalDate.AddDays(random.Next(1, 7));
            booking.Created = booking.ArrivalDate.Subtract(TimeSpan.FromDays(random.Next(1, 7)));
            booking.ModificationDate = booking.Created.AddDays(random.Next(1, 7));
            var rooms = _roomRepository.GetAll().ToList();
            booking.Room = rooms[random.Next(1, rooms.Count)];
            booking.Guests = new List<Guest>{
                new Guest(Age.Adult)
                {
                    FullName = booking.BookersName,
                    Email = booking.Email,
                    Country = booking.Country,
                    Citizenship = booking.Country,
                },
            };
            _bookings.Add(booking);
        }
    }

    public IEnumerable<Booking> GetAll()
    {
        return _bookings;
    }

    public Booking? Get(int id)
    {
        return _bookings.FirstOrDefault(booking => booking.Id == id);
    }

    public void Add(Booking booking)
    {
        _bookings.Add(booking);
    }

    public bool Delete(int id)
    {
        var booking = Get(id);
        if (booking != null)
            return _bookings.Remove(booking);
        return false;
    }

    public void Update(Booking booking)
    {
        var isDeleted = Delete(booking.Id);
        if (isDeleted)
            Add(booking);
    }
}
