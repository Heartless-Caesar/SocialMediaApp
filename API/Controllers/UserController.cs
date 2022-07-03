using System.Security.Claims;
using API.DTOs;
using API.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class UserController : BaseApiController
{
    private readonly IUserRepository _userRepository;

    private readonly IMapper _mapper;
    
    public UserController(IUserRepository userRepo, IMapper mapper)
    {
        _userRepository = userRepo;
        _mapper = mapper;
    }

    [HttpGet("/api/list")]
    public async Task<ActionResult<IEnumerable<MemberDTO>>> GetUsers()
    {
        var users = await _userRepository.GetMembersAsync();
        
        return Ok(_mapper.Map<IEnumerable<MemberDTO>>(users));
    }
    
     [HttpGet("/api/{username}")]
     public async Task<ActionResult<MemberDTO>> GetUser(string username){
         
         return await _userRepository.GetMemberByUsernameAsync(username);;
         
     }

     [HttpPut]
     public async Task<ActionResult> UpdateInfo(MemberUpdateDTO updateObj)
     {
         var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
         var user = await _userRepository.GetUserByUsernameAsync(username);

         _mapper.Map(updateObj,user);
         
         _userRepository.Update(user);

         if (await _userRepository.SaveAllAsync()) return NoContent();

         return BadRequest("Failed to update user");
     }
}