using ElProyecteGrande.Models;
using Newtonsoft.Json;
using OurNonfictionBackend.Models;

namespace OurNonfictionBackend.Data
{
    public static class DbInitializer
    {
        private static void CreateBookings(NonfictionContext context)
        {
            string json = File.ReadAllText(@"Data\newBooking.json");
            List<Booking> bookings = JsonConvert.DeserializeObject<List<Booking>>(json);
            context.Bookings.AddRange(bookings);
            context.SaveChanges();
        }

        public static void Initialize(NonfictionContext context)
        {
            if (context.Rooms.Any()&&context.Bookings.Any()&&context.Guests.Any())
            {
                return; //DB has been seeded
            }
            
            CreateBookings(context);
            
            

        }
    }
}
