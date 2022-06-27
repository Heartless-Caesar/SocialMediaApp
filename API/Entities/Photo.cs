using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities;

[Table("Photos")]
public class Photo
{
    [Key]
    public int Id { get; set; }

    public string URL { get; set; }

    public bool MainPicture { get; set; }

    public string PublicId { get; set; }
    
    public AppUser AppUser { get; set; }
    
    public int AppUserId { get; set; }
}