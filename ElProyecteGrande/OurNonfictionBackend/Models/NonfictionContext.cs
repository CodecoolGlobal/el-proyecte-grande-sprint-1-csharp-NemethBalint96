using ElProyecteGrande.Models;
using Microsoft.EntityFrameworkCore;

namespace OurNonfictionBackend.Models
{
    public class NonfictionContext : DbContext
    {
        public NonfictionContext(DbContextOptions<NonfictionContext> options) : base(options)
        {
        }

        public DbSet<Booking> Bookings => Set<Booking>();
        public DbSet<Guest> Guests => Set<Guest>();
        public DbSet<Room> Rooms => Set<Room>();
    }
}
