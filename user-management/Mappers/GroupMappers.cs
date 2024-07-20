using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using user_management.DTOs.Group;
using user_management.Models;

namespace user_management.Mappers
{
    public static class GroupMappers
    {
         public static GroupResponseDto toGroupDto(this Group groupModel)
        {
            return new GroupResponseDto{
                groupId=groupModel.groupId,
               name=groupModel.groupName,
               description=groupModel.description,
               members=groupModel.members.Select(u=>u.toUserDto()).ToList()
            };
        }
        public static Group reqToGroup(this GroupRequestDto dto)
        {
            return new Group{
                groupName=dto.name,
                description=dto.description
            };
        }
    }
}