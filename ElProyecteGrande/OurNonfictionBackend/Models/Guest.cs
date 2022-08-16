using System.ComponentModel.DataAnnotations.Schema;

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


    public Guest(Age age)
    {
        Age = age;
    }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string FullName { get; set; } = "Accompanying Guest";

    public DateTime BirthDate { get; set; }

    public string? BirthPlace { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? Address { get; set; }
    public int? PostalCode { get; set; }
    public string? Citizenship { get; set; }
    public string? Comment { get; set; }
    public Age Age { get; set; }
}
