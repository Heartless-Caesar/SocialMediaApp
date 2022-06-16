using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class AppContextDb : DbContext
{
    public AppContextDb(DbContextOptions options) : base(options) { }
    
    public DbSet<AppUser> Users { get; set;}
}