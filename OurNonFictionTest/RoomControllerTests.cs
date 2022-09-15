using ElProyecteGrande.Dal;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using OurNonfictionBackend.Controllers;
using OurNonfictionBackend.Models;

namespace OurNonFictionTest
{
    public class RoomControllerTests
    {
        private NonfictionContext _context;
        private RoomService _roomService;
        private RoomApiController _controller;

        [SetUp]
        public void Setup()
        {
            _context = new InitDatabase().CreateContext();
            _roomService = Substitute.For<RoomService>(_context);
            _controller = Substitute.For<RoomApiController>(_roomService);
        }

        [Test]
        public void RoomController_GetAll_ReturnsAllRooms()
        {
            var expected = _roomService.GetAll().Result.Count;
            var actual = _controller.GetAll().Result.Count;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void RoomController_GetAvailableRooms_ReturnsAllAvailabeleRooms()
        {
            var expected = _roomService.FilterRoomsByBookingDate(1).Result.Count;
            var actual = _controller.GetAvailableRooms(1).Result.Count;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void RoomController_AddNewRoomToBooking_AddsTheNewRoomToBooking()
        {
            var expected = _roomService.Get(1).Result.Id;
            var bookingId = 1;
            _controller.AddRoomToBooking(1, bookingId);
            var actual = _context.Bookings.Include(x => x.Room).First(x => x.Id == bookingId).Room.Id;
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
