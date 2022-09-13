using ElProyecteGrande.Models;
using NSubstitute;
using OurNonfictionBackend.Controllers;
using OurNonfictionBackend.Dal;
using OurNonfictionBackend.Models;

namespace OurNonFictionTest
{
    public class GuestControllerTest
    {
        private NonfictionContext _context;
        private GuestApiController _controller;
        private GuestService _guestService;

        [SetUp]
        public void Setup()
        {
            _context = Substitute.For<InitDatabase>().CreateContext();
            _guestService = Substitute.For<GuestService>(_context);
            _controller = Substitute.For<GuestApiController>(_guestService);
        }

        [Test]
        public void GuestController_GetAllNamedGuests_ReturnAllNamedGuests()
        {
            var expected = _guestService.GetAllNamedGuests().Result.Count();
            var actual = _controller.GetAllNamedGuests().Result.Count();
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GuestController_GetGuest_ReturnsTheGuest()
        {
            var expected = _guestService.GetGuest(1).Result.FullName;
            var actual = _controller.GetGuest(1).Result.FullName;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GuestController_AddNewGuestToBooking_ReturnsTheLatestGuest()
        {
            var newGuest = new Guest(Age.Adult)
            {
                FullName = "Kiss Bence",
                Address = "",
                PostalCode = null,
                BirthDate = DateTime.Now,
                BirthPlace = "Miskolc",
                Citizenship = "Hun",
                City = "Haha",
                Comment = "",
                Country = "Hun",
                Email = "kiss@kiss.com",
                Phone = "",
            };

            var actual = _controller.AddNewGuestToBooking(1, newGuest).Result.City;
            var expected = _guestService.GetLatestGuest().Result.City;
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
