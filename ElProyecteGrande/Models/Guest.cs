namespace ElProyecteGrande.Models;

public class Guest
{
    public Guid ID { get; set; }
    public string FullName { get; set; }
    public DateOnly BirthDate { get; set; }
    public string BirthPlace { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Adress { get; set; }
    public int PostalCode { get; set; }
    public string Citizenship { get; set; }
    public string MothersName { get; set; }
    public string Comment { get; set; }
}