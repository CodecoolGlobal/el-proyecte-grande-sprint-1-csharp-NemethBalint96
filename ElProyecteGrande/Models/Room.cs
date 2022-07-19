namespace ElProyecteGrande.Models;

public enum RoomType
{
    Apartman,
    Standard,
    Superior
}

public enum RoomStatus
{
    Active,
    Inactive
}

public class Room
{
    public int ID { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public RoomType RoomType { get; set; }
    public RoomStatus RoomStatus { get; set; }
    public int Floor { get; set; }
    public string Comment { get; set; }

    public string GetEnumValue(Enum myValue)
    {
        Enum myEnum = myValue;
        return myEnum.ToString();
    }
}