using System.ComponentModel.DataAnnotations;
using API.Entities;

namespace API.DTOs;

public class MemberDTO
{
    [Key]
    public int Id { get; set; }

    public string Username { get; set; }

    public byte[] PasswordHash { get; set; }

    public byte[] PasswordSalt { get; set; }
    
    public string KnownAs { get; set; }
    
    public string Gender { get; set; }
    
    public string LookingFor { get; set; }
    
    public string Introduction { get; set; }
    
    public string Interests { get; set; }
    
    public int Age { get; set; }

    public DateTime CreatedAt { get; set; } 

    public DateTime LastLogged { get; set; }

    public string City { get; set; }

    public string Country { get; set; }
    
    public ICollection<PhotoDTO> Photos { get; set; }
}