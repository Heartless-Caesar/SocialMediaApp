using API.Data;
using API.DTOs;
using API.Entities;
using API.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    public async Task<ActionResult<List<MemberDTO>>> GetUsers()
    {
        var users = await _userRepository.GetMembersAsync();
        
        return Ok(users);
    }
    
     [HttpGet("/api/{username}")]
     public async Task<ActionResult<MemberDTO>> GetUser(string username){
         
         return await _userRepository.GetMemberByUsernameAsync(username);;
         
     }
}