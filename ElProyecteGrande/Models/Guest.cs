using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ElProyecteGrande.Models;

public enum Age
{
    Adult,
    Child,
    Infant
}

public class Guest
{
    private static int _nextId = 1;

    public Guest()
    {

    }

    public Guest(Age age)
    {
        Age = age;
        ID = _nextId++;
    }

    public int ID { get; set; }
    public string FullName { get; set; } = "Accompanying Guest";
    [BindProperty, DataType(DataType.Date)]
    public DateOnly BirthDate { get; set; }
    public string BirthPlace { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Adress { get; set; }
    public int PostalCode { get; set; }
    public string Citizenship { get; set; }
    public string Comment { get; set; }
    public Age Age { get; set; }
}
