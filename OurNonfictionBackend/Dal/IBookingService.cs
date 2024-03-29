﻿using ElProyecteGrande.Models;

namespace ElProyecteGrande.Dal;
public interface IBookingService
{
    Task<List<Booking>> GetAll();
    Task<Booking>? Get(long bookingId);
    Task Add(Booking booking);
    Task Update(Booking booking, long bookingId);
    Task SetStatusCancelled(long bookingId);
    Task<Booking> GetLatestBooking();
}
