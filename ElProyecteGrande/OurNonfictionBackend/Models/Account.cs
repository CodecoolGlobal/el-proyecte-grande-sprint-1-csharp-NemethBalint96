using System.ComponentModel.DataAnnotations.Schema;

namespace OurNonfictionBackend.Models;

public class Account
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}