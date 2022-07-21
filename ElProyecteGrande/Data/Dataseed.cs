﻿using ElProyecteGrande.Dao;
using ElProyecteGrande.Models;

namespace ElProyecteGrande.Data
{
    public class Dataseed
    {
        public static void SetupInMemoryDatabase(BookingDaoMemory bookingDao)
        {
            bookingDao.Add(new Booking
            {
                ArrivalDate = DateTime.Now,
                DepartureDate = DateTime.Parse("2022.07.23"),
                BookersName = "Németh Bálint",
                Country = "Hungary",
                Email = "nemeth.balint1996@gmail.com",
                Adults = 4,
                Infants = 2,
                Children = 3,
                Guests = new List<Guest>{
                    new Guest(Age.Adult)
                    {
                        FullName = "Németh Bálint",
                        BirthDate = DateOnly.Parse("1996.01.01"),
                        BirthPlace = "Eger",
                        Email = "nemeth.balint1996@gmail.com",
                        Phone = "00000000",
                        Country = "Hungary",
                        City = "Füzesabony",
                        Address = "",
                        PostalCode = 3390,
                        Citizenship = "Hungary",
                    },
                    new Guest(Age.Infant)
                    {
                        BirthDate = DateOnly.Parse("2022.01.01"),
                    }
                },
                Room = new Room
                {
                    Comment = "  ",
                    Floor = 1,
                    DoorNumber = 2,
                    Price = 20
                },
                Status = Status.Confirmed,
            });
        }

    }
}
