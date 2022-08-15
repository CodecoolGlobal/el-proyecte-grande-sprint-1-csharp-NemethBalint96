using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElProyecteGrande.Models;

[JsonConverter(typeof(StringEnumConverter))]
public enum RoomType
{
    Apartman = 1,
    Standard = 2,
    Superior = 3
}

[System.Serializable]
public class Room
{


    public Room()
    {
    }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public int DoorNumber { get; set; }
    public decimal Price { get; set; }
    public RoomType RoomType { get; set; }
    public int Floor { get; set; }
    public string Comment { get; set; }
}
