namespace ElProyecteGrande.Models;

public enum Status
{
    Confirmed,
    Cancelled
}

public class Booking
{
    public Guid ID { get; set; }
    public string BookersName { get; set; }
    public List<Guest> Guests { get; set; }
    public string Email { get; set; }
    public Room Room { get; set; }
    public int Nights { get; set; }
    public int Adults { get; set; }
    public int Children { get; set; }
    public int Infants { get; set; }
    public decimal Total { get; set; }
    public string Country { get; set; }
    public DateOnly ArrivalDate { get; set; }
    public DateOnly DepartureDate { get; set; }
    public Status Status { get; set; }
    public DateTime Created { get; set; }
    public DateTime ModificationDate { get; set; }
}