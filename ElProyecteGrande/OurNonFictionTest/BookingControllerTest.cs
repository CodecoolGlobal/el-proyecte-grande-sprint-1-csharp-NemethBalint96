using ElProyecteGrande.Dal;
using ElProyecteGrande.Models;
using NSubstitute;
using NUnit.Framework.Constraints;
using OurNonfictionBackend.Controllers;
using OurNonfictionBackend.Models;

namespace OurNonFictionTest
{
    public class BookingControllerTest
    {
        private NonfictionContext _context;
        private BookingService _bookingService;
        private BookingApiController _controller;

        [SetUp]
        public void Setup()
        {
            _context = new InitDatabase().CreateContext();
            _bookingService = Substitute.For<BookingService>(_context);
            _controller = Substitute.For<BookingApiController>(_bookingService);
        }

        [Test]
        public void BookingController_GetAll_ReturnsAllBookings()
        {
            var expected = _bookingService.GetAll().Result.Count;
            var actual = _controller.GetAll().Result.Count;
            Assert.That(actual,Is.EqualTo(expected));
        }

        [Test]
        public void BookingController_GetBooking_ReturnsTheBooking()
        {
            var expected = _bookingService.Get(5).Result.Id;
            var actual = _controller.GetBooking(5).Result.Id;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void BookingController_AddNewBooking_ReturnsTheLatestBooking()
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
            var actual = _controller.AddNewBooking(newBooking).Result.Id;
            var expected = _bookingService.GetLatestBooking().Result.Id;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void BookingController_SetStatusToCancelled_SetBookingStatusToCancelled()
        {
            var expected = Status.Cancelled;
            var booking = _controller.SetStatusToCancelled(6);
            var actual = _bookingService.Get(6).Result.Status;
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
