using System.ComponentModel.DataAnnotations;
using API.Extentions;

namespace API.Entities;

public class AppUser
{
    [Key]
    public int Id { get; set; }

    public string Username { get; set; } = string.Empty;

    public byte[] PasswordHash { get; set; }

    public byte[] PasswordSalt { get; set; }
    
    public string KnownAs { get; set; }
    
    public string Gender { get; set; }
    
    public string LookingFor { get; set; }
    
    public string Introduction { get; set; }
    
    public string Interests { get; set; }
    
    public DateTime DateOfBirth { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime LastLogged { get; set; } = DateTime.Now;

    public string City { get; set; }

    public string Country { get; set; }
    
    public ICollection<Photo> Photos { get; set; }

    public int GetAge()
    {
        return DateOfBirth.CalculateAge();
    } 
}