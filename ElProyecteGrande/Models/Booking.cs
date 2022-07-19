using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ElProyecteGrande.Models;

public enum Status
{
    Confirmed,
    Cancelled
}

public class Booking
{
    public int ID { get; set; }
    public string BookersName { get; set; }
    public List<Guest> Guests { get; set; }
    public string Email { get; set; }
    public Room Room { get; set; }

    public int Adults { get; set; }

    public int Children { get; set; }
    public int Infants { get; set; }

    public decimal Total => Nights * Room.Price;
    public string Country { get; set; }
    [BindProperty, DataType(DataType.Date), DisplayFormat(DataFormatString = "{yyyy.MM.dd}", ApplyFormatInEditMode = true)]
    public DateTime ArrivalDate { get; set; }
    [BindProperty, DataType(DataType.Date), DisplayFormat(DataFormatString = "{yyyy.MM.dd}", ApplyFormatInEditMode = true)]
    public DateTime DepartureDate { get; set; }
    public int Nights => Math.Abs(DepartureDate.Day - ArrivalDate.Day);
    public Status Status { get; set; }
    public DateTime Created =>DateTime.Now;
    public DateTime ModificationDate { get; set; }

    public string GetEnumValue(Enum myValue)
    {
        Enum myEnum = myValue;
        return myEnum.ToString();
    }
}