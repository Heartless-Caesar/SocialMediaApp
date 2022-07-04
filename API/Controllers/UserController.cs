using System.Security.Claims;
using API.DTOs;
using API.Entities;
using API.Extentions;
using API.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.EnvironmentVariables;

namespace API.Controllers;

[Authorize]
public class UserController : BaseApiController
{
    private readonly IUserRepository _userRepository;

    private readonly IMapper _mapper;

    private readonly IPhotoService _photoService;
    
    //Dependencies
    public UserController(IUserRepository userRepo, IMapper mapper, IPhotoService photoService)
    {
        _userRepository = userRepo;
        _mapper = mapper;
        _photoService = photoService;
    }

    [HttpGet("/api/list")]
    public async Task<ActionResult<IEnumerable<MemberDTO>>> GetUsers()
    {
        var users = await _userRepository.GetMembersAsync();
        
        //The Ok response is added due to returning the mapped enumerable  
        //not being accepted by the ActionResult
        return Ok(_mapper.Map<IEnumerable<MemberDTO>>(users));
    }
    
     [HttpGet("/api/{username}")]
     //Fetches user based on their username
     public async Task<ActionResult<MemberDTO>> GetUser(string username){
         
         return await _userRepository.GetMemberByUsernameAsync(username);;
         
     }

     [HttpPut("/api/update")]
     public async Task<ActionResult> UpdateInfo(MemberUpdateDTO updateObj)
     {
         //Gets the current user's username as it is registered in the claims
         var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());
         
         //Maps the updated data to the user
         _mapper.Map(updateObj,user);
         
         //Updates the updated user entity
         _userRepository.Update(user);
         
         //Saves changes 
         if (await _userRepository.SaveAllAsync()) return NoContent();
         
         //Error in case something does wrong
         return BadRequest("Failed to update user");
     }


     [HttpPost("/api/add-photo")]
     public async Task<ActionResult<PhotoDTO>> AddPhoto(IFormFile file)
     {
         //Gets current username due to being authorized
         var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());
         
         //Adds photo to Cloudinary
         var result = await _photoService.AddPhotoAsync(file);
         
         //In case of an error while uploading to Cloudinary
         if (result.Error != null) return BadRequest(result.Error.Message);
         
         //New photo's attributes based on the uploaded photo
         var photo = new Photo
         {
             Url = result.SecureUrl.AbsoluteUri,
             Publicid = result.PublicId
         };
         
         //Sets photo as main/profile picture if there were previously no photos
         if (user.Photos.Count == 0)
         {
             photo.isMain = true;
         }
         
         //Adds photo to the user Photo collection
         user.Photos.Add(photo);
         
         //Maps the added photo to a PhotoDTO
         if (await _userRepository.SaveAllAsync())
         {
             return _mapper.Map<PhotoDTO>(photo);
         }
         
         //Error if something goes wrong while saving changes 
         return BadRequest("Problem adding photo");
     }
}