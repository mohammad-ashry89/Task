using Microsoft.EntityFrameworkCore;
using user_management.Data;
using user_management.DTOs.Group;
using user_management.Interfaces;
using user_management.Models;

namespace user_management.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly ApplicationDBContext _context;
        public GroupRepository(ApplicationDBContext context)
        {
            _context=context;
        }
        public async Task<Group> addGroupAsync(Group g)
        {
        await _context.groups.AddAsync(g);
       await _context.SaveChangesAsync();
       return g;
        }

        public async Task<Group?> deleteGroup(string name)
        {
            var group=await _context.groups.FirstOrDefaultAsync(g=>g.groupName==name);
            if(group==null)
            {
            return null;
            }
              _context.groups.Remove(group);
         await _context.SaveChangesAsync();
         return group;
        }

        public async Task<List<Group>> getAllGroups()
        {
            return await _context.groups.ToListAsync();
        }

        public  async Task<Group?> getGroupByName(string name)
        {
            var group=await _context.groups.FirstOrDefaultAsync(g=>g.groupName==name);
            if(group==null)
            return null;
            return group;
        }

        public async Task<Group?> upddateGroup(string name, GroupUpdateDto dto)
        {
           var group=await _context.groups.FirstOrDefaultAsync(x=>x.groupName==name);
           if(group==null)
           return null;
            if(dto.name!=null && dto.name!="")
        group.groupName=dto.name;
        if(dto.description!=null && dto.description!="")
        group.description=dto.description;
         await _context.SaveChangesAsync();
         return group;
        }
    }
}