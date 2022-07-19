using ElProyecteGrande.Models;

namespace ElProyecteGrande.Dao
{
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
            _bookings.Add(booking);
        }

        public IEnumerable<Booking> GetAll()
        {
            return _bookings;
        }
    }
}
