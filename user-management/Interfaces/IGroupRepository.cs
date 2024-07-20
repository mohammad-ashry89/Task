using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using user_management.DTOs.Group;
using user_management.Models;

namespace user_management.Interfaces
{
    public interface IGroupRepository
    {
        Task<List<Group>> getAllGroups();
        Task<Group?> getGroupByName(string name);
        Task<Group> addGroupAsync(Group g);
        Task<Group?> upddateGroup(String name,GroupUpdateDto dto);
        Task<Group?> deleteGroup(String name);
        
    }
}