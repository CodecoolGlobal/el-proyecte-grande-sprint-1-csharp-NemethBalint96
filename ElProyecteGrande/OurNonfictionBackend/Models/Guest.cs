using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ElProyecteGrande.Models;
public enum Age
{
    Adult,
    Child,
    Infant
}
[System.Serializable]
public class Guest
{
    private static int _nextId = 1;

    public Guest()
    {

    }

    public Guest(Age age)
    {
        Age = age;
        Id = _nextId++;
    }

    public int Id { get; set; }
    public string FullName { get; set; } = "Accompanying Guest";
    [BindProperty, DataType(DataType.DateTime)]
    public DateTime BirthDate { get; set; }
    public string BirthPlace { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public int PostalCode { get; set; }
    public string Citizenship { get; set; }
    public string Comment { get; set; }
    public Age Age { get; set; }
}
