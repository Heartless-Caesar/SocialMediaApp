using API.DTOs;
using API.Entities;
using API.Interface;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class UserRepository : IUserRepository
{

    private readonly AppContextDb _context;

    private readonly IMapper _mapper;
    //CONSTRUCTOR
    public UserRepository(AppContextDb context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Update(AppUser userObj)
    {
         _context.Entry(userObj).State = EntityState.Modified;
    }

    public async Task<bool> SaveAllAsync()
    {
        //The SaveChangesAsync returns the number of changes 
        //that have been made, so if it returns 0 no changes 
        //have been made, so there is nothing to save
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<AppUser>> GetUsersAsync()
    {
        //Returns all user entities alongside their photo collection
        //Reminder that the entities will be mapped to the UserDTO
        //As shown in AutoMapperProfiles.cs so that the circular
        //Referencing doesn't happen
        return await _context.Users
            .Include(p => p.Photos)
            .ToListAsync();
    }

    public async Task<AppUser> GetUserByIdAsync(int id)
    {
        //Returns an user entity alongside their photo collection
        //Reminder that the entities will be mapped to the UserDTO
        //As shown in AutoMapperProfiles.cs so that the circular
        //Referencing doesn't happen
        return await _context.Users.FindAsync(id);
    }

    public async Task<AppUser> GetUserByUsernameAsync(string username)
    {
        //Returns an user entity based on their username alongside
        //their photo collection
        //Reminder that the entities will be mapped to the UserDTO
        //As shown in AutoMapperProfiles.cs so that the circular
        //Referencing doesn't happen
        return await _context.Users
            .Include(p => p.Photos)
            .SingleOrDefaultAsync(x => x.Username.ToLower() == username);
    }

    public async Task<MemberDTO> GetMemberByUsernameAsync(string username)
    {
        //Returns a member entity by their username
        //alongside their photo collection
        //Reminder that the entities will be mapped to the UserDTO
        //As shown in AutoMapperProfiles.cs so that the circular
        //Referencing doesn't happen
        return await _context.Users.Where(x => x.Username == username.ToLower())
            .ProjectTo<MemberDTO>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<MemberDTO>> GetMembersAsync()
    {
        return await _context.Users.ProjectTo<MemberDTO>(_mapper.ConfigurationProvider).ToListAsync();
    }
}