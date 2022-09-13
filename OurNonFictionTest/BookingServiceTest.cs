using ElProyecteGrande.Dal;
using ElProyecteGrande.Models;
using NSubstitute;
using OurNonfictionBackend.Models;

namespace OurNonFictionTest
{
    public class BookingServiceTest
    {
        private NonfictionContext _context;
        private BookingService _bookingService;

        [SetUp]
        public void Setup()
        {
            _context = Substitute.For<InitDatabase>().CreateContext();
            _bookingService = Substitute.For<BookingService>(_context);
        }

        [Test]
        public void BookingService_GetAll_ReturnsAllBookings()
        {
            var expected = _context.Bookings.Count();
            var actual = _bookingService.GetAll().Result.Count;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void BookingService_Get_ReturnsBookingById()
        {
            var bookingId = 9;
            var expected = _context.Bookings.First(booking => booking.Id == bookingId).Id;
            var actual = _bookingService.Get(bookingId).Result.Id;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void BookingService_Add_AddsNewBookingToDatabase()
        {
            var newBooking = new Booking
            {
                BookersName = "Kiss Bence",
                Guests = null,
                Email = "kissbence19@gmail.com",
                Room = null,
                Adults = 0,
                Children = 0,
                Infants = 0,
                Country = "Hun",
                ArrivalDate = DateTime.Now,
                DepartureDate = DateTime.Now.AddDays(1),
                Status = Status.Confirmed,
                Created = DateTime.Now,
                ModificationDate = DateTime.Now
            };
            var expected = _context.Bookings.Last().Id + 1;
            _bookingService.Add(newBooking);
            var actual = _context.Bookings.Last().Id;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void BookingService_Update_UpdatesBooking()
        {
            var bookingId = 10;
            var booking = _context.Bookings.First(booking => booking.Id == bookingId);
            booking.BookersName = "Kiss Bence";
            _bookingService.Update(booking, bookingId);
            var expected = booking.BookersName;
            var actual = _bookingService.Get(bookingId).Result.BookersName;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void BookingService_SetStatusCancelled_SetBookingStatusCancelled()
        {
            var bookingId = 4;
            var expected = Status.Cancelled;
            _bookingService.SetStatusCancelled(bookingId);
            var actual = _bookingService.Get(bookingId).Result.Status;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void BookingService_GetLatestBooking_ReturnsTheLastBooking()
        {
            var expected = _context.Bookings.Last().Id;
            var actual = _bookingService.GetLatestBooking().Result.Id;
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
