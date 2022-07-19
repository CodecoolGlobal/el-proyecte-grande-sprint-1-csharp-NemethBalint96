namespace ElProyecteGrande.Models;

public enum RoomType
{
    Apartman,
    Standard,
    Superior
}

public class Room
{
    private static int _nextId = 1;
    public Room()
    {
        ID = _nextId++;
    }

    public int ID { get; }
    public int DoorNumber { get; set; }
    public decimal Price { get; set; }
    public RoomType RoomType { get; set; }
    public int Floor { get; set; }
    public string Comment { get; set; }
}