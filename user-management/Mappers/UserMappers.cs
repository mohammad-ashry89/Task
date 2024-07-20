using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using user_management.DTOs.User;
using user_management.Models;

namespace user_management.Mappers
{
    public static class UserMapper
    {
        public static UserResponseDto toUserDto(this User userModel)
        {
#pragma warning disable CS8601 // Possible null reference assignment.
#pragma warning disable CS8601 // Possible null reference assignment.
            return new UserResponseDto{
                firstName=userModel.firstName,
                lastName=userModel.lastName,
                email=userModel.Email,
                groupId=userModel.groupId,
                username=userModel.UserName,
            };
#pragma warning restore CS8601 // Possible null reference assignment.
#pragma warning restore CS8601 // Possible null reference assignment.
        }
        public static User reqDtoToUser(this UserRequestDto dto )
    {
        return new User{
            firstName=dto.firstName,
            lastName = dto.lastName,
            Email=dto.email,
            UserName=dto.username
        };
    }
    }
    
}