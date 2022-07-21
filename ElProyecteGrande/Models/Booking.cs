using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ElProyecteGrande.Models;

public enum Status
{
    Confirmed,
    Cancelled
}

public class Booking
{
    public static int NextId = 0;
    public Booking()
    {
        Guests = new List<Guest>();
        Created = DateTime.Now;
    }

    public int ID { get; set; }
    public string BookersName { get; set; }
    public List<Guest> Guests { get; set; }
    public string Email { get; set; }
    public Room Room { get; set; }
    public int Adults { get; set; }
    public int Children { get; set; }
    public int Infants { get; set; }
    public decimal Total => Nights * Room.Price * Adults;
    public string Country { get; set; }
    [BindProperty, DataType(DataType.Date)]
    public DateTime ArrivalDate { get; set; }
    [BindProperty, DataType(DataType.Date)]
    public DateTime DepartureDate { get; set; }
    public int Nights => (DepartureDate - ArrivalDate).Days;
    public Status Status { get; set; }
    public DateTime Created { get; }
    public DateTime ModificationDate { get; set; }
}
