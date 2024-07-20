
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using user_management.Data;
using user_management.DTOs.User;
using user_management.Interfaces;
using user_management.Mappers;
using user_management.Models;

namespace user_management.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController: ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepo;
        private readonly IGroupRepository _groupRepo;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;
        public UserController(IUserRepository userRepo,UserManager<User> userManager,IGroupRepository repo,ITokenService tokenService,SignInManager<User> signinManager)
        {
            _groupRepo=repo;
            _userManager = userManager;
            _userRepo=userRepo;
            _tokenService=tokenService;
            _signInManager=signinManager;
        }
        [HttpGet("allusers")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> getAllUsers()
        {
            var res=await _userRepo.getAllUsersAsync();
            var users=res.Select(s=>s.toUserDto());
            return Ok(res);
        }
        [HttpGet("getuser/{username}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> getUser([FromRoute] String username)
        {
            if(!ModelState.IsValid)
        return BadRequest(ModelState);
            var user =await  _userRepo.getUserByUsernameAsync(username);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(user.toUserDto()); 
            }
        }
        [HttpPost("register")]
        public async Task<IActionResult> addUser([FromBody] UserRequestDto requsetDto)
        {
            try
            {
            if(!ModelState.IsValid)
        return BadRequest(ModelState);
            var user= requsetDto.reqDtoToUser();
           var createdUser=await _userManager.CreateAsync(user,requsetDto.password);
           if(createdUser.Succeeded)
           {
            var group= await _groupRepo.getGroupByName("unassigned");
            if(group == null)
            {
              await _groupRepo.addGroupAsync(new Group{
                groupName="unassigned",
                description="Group for unassigned users"
              });
            }
            var roleResult= await _userManager.AddToRoleAsync(user,"User");
            if(roleResult.Succeeded)
            {
#pragma warning disable CS8604 // Possible null reference argument.
                    await assignGroup(user.UserName,"unassigned");
#pragma warning restore CS8604 // Possible null reference argument.
                

            //return CreatedAtAction(nameof(getUser), new {username=user.UserName},user.toUserDto());
            return Ok("user registered");
            }
            else
            return StatusCode(500,roleResult.Errors);
           }
            else
            return StatusCode(500,createdUser.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }
[HttpPost("login")]
public async Task<IActionResult> login(LoginDto dto)
{
  if(!ModelState.IsValid)
  return BadRequest(ModelState);
  var user = await _userManager.Users.FirstOrDefaultAsync(x=>x.UserName==dto.UserName.ToLower());
  if(user==null)
  return Unauthorized("invalid username");
var result=await _signInManager.CheckPasswordSignInAsync(user,dto.Password,false);
if(!result.Succeeded)
{
return Unauthorized("invalid username/password");
}
var roleName=await _userManager.GetRolesAsync(user);

 Console.WriteLine("User role is "+roleName[0].ToString());
#pragma warning disable CS8601 // Possible null reference assignment.
#pragma warning disable CS8601 // Possible null reference assignment.
return Ok(new LoggedUserDto{
  UserName=user.UserName,
  email=user.Email,
  role=roleName[0].ToString(),
  token=await _tokenService.createToken(user)
});
#pragma warning restore CS8601 // Possible null reference assignment.
#pragma warning restore CS8601 // Possible null reference assignment.
  
}

    
    [HttpPut("updateuser/{username:}")]
    [Authorize]
      public async Task<IActionResult> updateUser([FromRoute]String username,[FromBody]UserUpdateDto dto)
      {
        if(!ModelState.IsValid)
        return BadRequest(ModelState);
        var user =  await _userRepo.updateUser(username,dto);
        if(user == null)
        return NotFound();
        
        return Ok(user.toUserDto());
      }
      [HttpPut("assignGroup/{username}/{groupName}")]
      [Authorize(Roles ="Admin")]
      public async Task<IActionResult> assignGroup([FromRoute]String username,[FromRoute]String groupName)
      {
        if(!ModelState.IsValid)
        return BadRequest(ModelState);
        User user =await  _userRepo.assignGroup(username,groupName);
        if(user == null)
        return NotFound();
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        Group group = await _groupRepo.getGroupByName(groupName);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        if(group==null)
        return NotFound();
         await _userManager.RemoveFromRolesAsync(user,await _userManager.GetRolesAsync(user));
        if(groupName=="admins")
        {
           
          await _userManager.AddToRoleAsync(user,"Admin");
        }
           else if(groupName=="unassigned")
           await _userManager.AddToRoleAsync(user,"User");
         else
          await _userManager.AddToRoleAsync(user,"Developer");
         
        return Ok(user.toUserDto());
      }
      [HttpDelete("deleteuser/{username}")]
      [Authorize(Roles ="Admin")]
      public async Task<IActionResult> deleteUser([FromRoute]String username)
      {
        if(!ModelState.IsValid)
        return BadRequest(ModelState);
        var user=  await _userRepo.deleteUser(username);
        if(user==null)
        return NotFound();
         
        return NoContent();
      }


}
}