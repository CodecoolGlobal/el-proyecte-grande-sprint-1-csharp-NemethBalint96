using System.ComponentModel.DataAnnotations.Schema;

namespace ElProyecteGrande.Models;
public enum Status
{
    Confirmed,
    Cancelled
}
[System.Serializable]
public class Booking
{


    public Booking()
    {

        Guests = new List<Guest>();
        Created = DateTime.Now;
    }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string BookersName { get; set; }
    public List<Guest> Guests { get; set; }
    public string Email { get; set; }
    public Room? Room { get; set; }
    public int Adults { get; set; }
    public int Children { get; set; }
    public int Infants { get; set; }
    public string Country { get; set; }
    public DateTime ArrivalDate { get; set; }
    public DateTime DepartureDate { get; set; }
    public int Nights => (DepartureDate - ArrivalDate).Days;
    public decimal Total
    {
        get
        {
            if (Room is null)
            {
                return 0;
            }
            return Nights * Room.Price;

        }
    }
    public Status Status { get; set; }
    public DateTime Created { get; set; }
    public DateTime ModificationDate { get; set; }

    public string CalculatePrice()
    {
        return ((Adults + Children + Infants) * Room.Price * Nights).ToString();
    }
}
