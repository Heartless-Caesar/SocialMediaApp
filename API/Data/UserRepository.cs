using API.Entities;
using API.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class UserRepository : IUserRepository
{

    private readonly AppContextDb _context;
    
    //CONSTRUCTOR
    public UserRepository(AppContextDb context)
    {
        _context = context;
    }

    public void Update(AppUser userObj)
    {
         _context.Entry(userObj).State = EntityState.Modified;
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<AppUser>> GetUsersAsync()
    {
        return await _context.Users
            .Include(p => p.Photos)
            .ToListAsync();
    }

    public async Task<AppUser> GetUserByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<AppUser> GetUserByUsernameAsync(string username)
    {
        return await _context.Users
            .Include(p => p.Photos)
            .SingleOrDefaultAsync(x => x.Username.ToLower() == username);
    }
}