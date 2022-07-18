namespace ElProyecteGrande.Models;

public enum RoomType
{
    Apartman = 4,
    Standard = 2,
    Superior = 3
}

public enum RoomStatus
{
    Active,
    Inactive
}

public class Room
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public RoomType RoomType { get; set; }
    public RoomStatus RoomStatus { get; set; }
    public int Floor { get; set; }
    public string Comment { get; set; }
}