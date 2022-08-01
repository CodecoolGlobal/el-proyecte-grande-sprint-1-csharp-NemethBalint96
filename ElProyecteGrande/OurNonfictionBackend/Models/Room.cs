﻿namespace ElProyecteGrande.Models;

public enum RoomType
{
    Apartman,
    Standard,
    Superior
}
[System.Serializable]
public class Room
{
    private static int _nextId = 1;
    public Room()
    {
        Id = _nextId++;
    }

    public int Id { get; set; }
    public int DoorNumber { get; set; }
    public decimal Price { get; set; }
    public RoomType RoomType { get; set; }
    public int Floor { get; set; }
    public string Comment { get; set; }
}