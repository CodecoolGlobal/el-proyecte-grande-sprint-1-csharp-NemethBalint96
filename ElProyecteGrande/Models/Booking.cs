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
    public decimal Total { get; set; }
    public string Country { get; set; }
    public DateOnly ArrivalDate { get; set; }
    public DateOnly DepartureDate { get; set; }
    public int Nights => DepartureDate.Day - ArrivalDate.Day;
    public Status Status { get; set; }
    public DateTime Created { get; set; }
    public DateTime ModificationDate { get; set; }

    public string GetEnumValue(Enum myValue)
    {
        Enum myEnum = myValue;
        return myEnum.ToString();
    }
}