using ElProyecteGrande.Models;

namespace ElProyecteGrande.Dao;

public class BookingDaoMemory
{
    private List<Booking> _bookings;
    private static BookingDaoMemory _instance;
    private BookingDaoMemory()
    {
        _bookings = new List<Booking>();
    }

    public static BookingDaoMemory GetInstance()
    {
        if (_instance == null)
        {
            _instance = new BookingDaoMemory();
        }

        return _instance;
    }

    public void Add(Booking booking)
    {
        booking.ID = ++Booking.NextId;
        for (var i = 0; i < booking.Adults; i++)
        {
            var adult = new Guest(Age.Adult);
            booking.Guests.Add(adult);
        }
        for (var i = 0; i < booking.Children; i++)
        {
            var child = new Guest(Age.Child);
            booking.Guests.Add(child);
        }
        for (var i = 0; i < booking.Infants; i++)
        {
            var infant = new Guest(Age.Infant);
            booking.Guests.Add(infant);
        }

        _bookings.Add(booking);
    }

    public IEnumerable<Booking> GetAll()
    {
        return _bookings;
    }

    public Booking? Get(int id)
    {
        var booking = _bookings.FirstOrDefault(booking => booking.ID == id);
        return booking ?? null;
    }

    public void SetStatusCancelled(int Id)
    {
        var booking = _bookings.FirstOrDefault(x => x.ID == Id);
        booking.Status = Status.Cancelled;
    }

    public void Edit(Booking booking)
    {
        var editableBooking = Get(booking.ID);
        //editableBooking = booking;
        
        editableBooking.ModificationDate = DateTime.Now;
    }
}