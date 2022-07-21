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
        CreateGuests(booking.Adults,booking.Children,booking.Infants,booking.Guests);

        _bookings.Add(booking);
    }

    private void CreateGuests(int adults,int children,int infants,List<Guest> guests)
    {
        for (var i = 0; i < adults; i++)
        {
            var adult = new Guest(Age.Adult);
            guests.Add(adult);
        }

        for (var i = 0; i < children; i++)
        {
            var child = new Guest(Age.Child);
            guests.Add(child);
        }

        for (var i = 0; i < infants; i++)
        {
            var infant = new Guest(Age.Infant);
            guests.Add(infant);
        }
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
        CreatePlusGuests(booking, editableBooking);
        editableBooking.BookersName = booking.BookersName;
        editableBooking.Email = booking.Email;
        editableBooking.Country = booking.Country;
        editableBooking.Adults = booking.Adults;
        editableBooking.Children = booking.Children;
        editableBooking.Infants = booking.Infants;
        editableBooking.ArrivalDate = booking.ArrivalDate;
        editableBooking.DepartureDate = booking.DepartureDate;
        editableBooking.Room.Comment = booking.Room.Comment;
        editableBooking.ModificationDate = DateTime.Now;

       
    }

    private void CreatePlusGuests(Booking booking, Booking? editableBooking)
    {
        var adultsnumber = booking.Adults -editableBooking.Adults;
        var childrennumber = booking.Children - editableBooking.Children;
        var infantsnumber = booking.Infants - editableBooking.Infants;
        CreateGuests(adultsnumber, childrennumber,
            infantsnumber, editableBooking.Guests);
    }

    public void DeleteGuestFromBooking(int bookingId, int guestId)
    {
        var guests = _bookings.Find(id => id.ID == bookingId).Guests;
        var guest = guests.Find(x => x.ID == guestId);
        DecreaseGuestNumber(bookingId, guest);

        guests.Remove(guest);
       
    }

    private void DecreaseGuestNumber(int bookingId, Guest guest)
    {
        var booking = _bookings.Find(id => id.ID == bookingId);
        switch (guest.Age)
        {
            case Age.Adult:
                booking.Adults--;
                break;
            case Age.Child:
                booking.Children--;
                break;
            case Age.Infant: 
                booking.Infants--;
                break;
        }
    }
}