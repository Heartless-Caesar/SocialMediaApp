using API.DTOs;
using API.Entities;

namespace API.Interface;

public interface IUserRepository
{
    void Update(AppUser userObj);
    
    Task<bool> SaveAllAsync();

    Task<IEnumerable<AppUser>> GetUsersAsync();

    Task<AppUser> GetUserByIdAsync(int id);
    
    Task<AppUser> GetUserByUsernameAsync(string username);

    Task<MemberDTO> GetMemberByUsernameAsync(string username);

    Task<IEnumerable<MemberDTO>> GetMembersAsync();

}