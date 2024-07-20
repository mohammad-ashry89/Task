
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using user_management.Data;
using user_management.DTOs.Group;
using user_management.Interfaces;
using user_management.Mappers;

namespace user_management.Controllers
{
    [Route("api/group")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IGroupRepository _groupRepo;
        public GroupController(ApplicationDBContext context, IGroupRepository groupRepo)
        {
            _context=context;
            _groupRepo=groupRepo;
        }
    
    [HttpGet("allgroups")]
    [Authorize]
    public async Task<IActionResult> getAllGroups()
    {
        var res= await _groupRepo.getAllGroups();
         var groups=res.Select(s=>s.toGroupDto());
        return Ok(groups);
    }

    [HttpGet("getgroup/{groupName}")]
    [Authorize]
    public  async Task<IActionResult> getGroup([FromRoute] string groupName)
    {
        if(!ModelState.IsValid)
        return BadRequest(ModelState);
        var group=await  _groupRepo.getGroupByName(groupName);
        if(group==null)
        return NotFound();
        else
        return Ok(group);
    }
    [HttpPost("addgroup")]
    [Authorize(Roles ="Admin")]
    public async Task<IActionResult> addGroup([FromBody] GroupRequestDto dto)
    {
        if(!ModelState.IsValid)
        return BadRequest(ModelState);
        var group= dto.reqToGroup();
       await _groupRepo.addGroupAsync(group);
        return CreatedAtAction(nameof(getGroup), new {name=group.groupName},group.toGroupDto());
    }
    [HttpPut("updategroup/{name}")]
    [Authorize(Roles ="Admin")]
    public  IActionResult updateGroup([FromRoute]String name,[FromBody] GroupUpdateDto dto)
    {
        if(!ModelState.IsValid)
        return BadRequest(ModelState);
        var group= _groupRepo.upddateGroup(name,dto);
        if(group==null)
        return NotFound();
       
        return Ok(group);  
    }

    [HttpDelete("deletegroup/{name}")]
    [Authorize(Roles ="Admin")]
      public  async Task<IActionResult> deleteGroup([FromRoute]String name)
      {
        if(!ModelState.IsValid)
        return BadRequest(ModelState);
        var group= await _groupRepo.deleteGroup(name);
        if(group==null)
        return NotFound();
       
        return NoContent();
      }
} 
}