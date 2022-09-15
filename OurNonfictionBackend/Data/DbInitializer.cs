﻿using ElProyecteGrande.Models;
using OurNonfictionBackend.Models;

namespace OurNonfictionBackend.Data
{
    public static class DbInitializer
    {


        public static void Initialize(NonfictionContext context)
        {
            if (context.Rooms.Any() && context.Bookings.Any() && context.Guests.Any() && context.Accounts.Any())
            {
                return; //DB has been seeded
            }
            CreateAccount(context);
            CreateRooms(context);
        }

        private static void CreateAccount(NonfictionContext context)
        {
            context.Accounts.Add(new Account()
            { Email = "admin@admin.com", Username = "admin", Password = BCrypt.Net.BCrypt.HashPassword("admin"), Role = "Admin" });
            context.SaveChanges();
        }

        private static void CreateRooms(NonfictionContext context)
        {
            for (var i = 1; i < 6; i++)
            {
                var room = new Room
                {
                    DoorNumber = i,
                    Floor = 1,
                    RoomType = RoomType.Standard,
                    Price = 80
                };
                context.Rooms.Add(room);
            }
            for (var i = 1; i < 6; i++)
            {
                var room = new Room
                {
                    DoorNumber = i,
                    Floor = 2,
                    RoomType = RoomType.Superior,
                    Price = 100
                };
                context.Rooms.Add(room);
            }
            for (var i = 1; i < 6; i++)
            {
                var room = new Room
                {
                    DoorNumber = i,
                    Floor = 3,
                    RoomType = RoomType.Apartman,
                    Price = 150
                };
                context.Rooms.Add(room);
                
            }
            context.SaveChanges();
        }

    }
}
