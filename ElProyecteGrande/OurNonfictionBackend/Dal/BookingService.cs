using ElProyecteGrande.Models;

namespace ElProyecteGrande.Dal;
public class BookingService : IBookingService
{
    private readonly IRepository<Booking> _bookingRepository;

    public BookingService(IRepository<Booking> repository)
    {
        _bookingRepository = repository;
    }

    public IEnumerable<Booking> GetAll()
    {
        return _bookingRepository.GetAll();
    }

    public Booking? Get(int bookingId)
    {
        return _bookingRepository.Get(bookingId);
    }

    public void Add(Booking booking)
    {
        CreateGuests(booking.Adults, booking.Children, booking.Infants, booking.Guests);
        _bookingRepository.Add(booking);
    }

    private void CreateGuests(int adults, int children, int infants, List<Guest> guests)
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

    public void SetStatusCancelled(int bookingId)
    {
        var booking = _bookingRepository.Get(bookingId);
        booking.Status = Status.Cancelled;
        _bookingRepository.Update(booking);
    }

    public void Update(Booking booking)
    {
        var oldBooking = _bookingRepository.Get(booking.Id);
        CreatePlusGuests(oldBooking, booking);
        _bookingRepository.Update(booking);
    }

    private void CreatePlusGuests(Booking booking, Booking? editableBooking)
    {
        var adultsNumber = editableBooking.Adults - booking.Adults;
        var childrenNumber = editableBooking.Children - booking.Children;
        var infantsNumber = editableBooking.Infants - booking.Infants;
        CreateGuests(adultsNumber, childrenNumber,
            infantsNumber, editableBooking.Guests);
    }

    public void DeleteGuestFromBooking(int guestId)
    {
        var booking = _bookingRepository.GetAll()
            .FirstOrDefault(booking => booking.Guests.Any(guest => guest.Id == guestId));
        var guests = _bookingRepository.Get(booking.Id).Guests;
        var guest = guests.Find(x => x.Id == guestId);
        DecreaseGuestNumber(booking.Id, guest);

        guests.Remove(guest);
    }

    private void DecreaseGuestNumber(int bookingId, Guest guest)
    {
        var booking = _bookingRepository.Get(bookingId);
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

    public Guest GetGuest(int guestId)
    {
        return _bookingRepository.GetAll().SelectMany(booking => booking.Guests).First(guest => guest.Id == guestId);
    }

    public void EditGuest(Guest newGuest)
    {
        var editableGuest = GetGuest(newGuest.Id);
        editableGuest.Id = newGuest.Id;
        editableGuest.FullName = newGuest.FullName;
        editableGuest.BirthDate = newGuest.BirthDate;
        editableGuest.BirthPlace = newGuest.BirthPlace;
        editableGuest.Email = newGuest.Email;
        editableGuest.Phone = newGuest.Phone;
        editableGuest.Country = newGuest.Country;
        editableGuest.City = newGuest.City;
        editableGuest.Address = newGuest.Address;
        editableGuest.PostalCode = newGuest.PostalCode;
        editableGuest.Citizenship = newGuest.Citizenship;
        editableGuest.Comment = newGuest.Comment;
        editableGuest.Age = newGuest.Age;
    }

    public IEnumerable<Guest> GetAllNamedGuests()
    {
        return _bookingRepository.GetAll()
            .SelectMany(b => b.Guests.Where(guest => guest.FullName != "Accompanying Guest"));
    }
}
