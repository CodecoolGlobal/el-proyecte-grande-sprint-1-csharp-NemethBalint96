using ElProyecteGrande.Models;
using Newtonsoft.Json;

namespace ElProyecteGrande.Dal;
public class BookingRepository : IRepository<Booking>
{
    private  List<Booking> _bookings;
    public BookingRepository()
    {
        _bookings = new List<Booking>();
        CreateBookings();
    }

    private void CreateBookings()
    {
        string json = File.ReadAllText(@"Data\newBooking.json");
        List<Booking> bookings = JsonConvert.DeserializeObject<List<Booking>>(json);
        foreach (var booking in bookings)
        { 
            _bookings.Add(booking);
        }
    }

    public IEnumerable<Booking> GetAll()
    {
        return _bookings;
    }

    public Booking? Get(int bookingId)
    {
        return _bookings.FirstOrDefault(booking => booking.Id == bookingId);
    }

    public void Add(Booking booking)
    {
        _bookings.Add(booking);
    }

    public bool Delete(int bookingId)
    {
        var booking = Get(bookingId);
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
